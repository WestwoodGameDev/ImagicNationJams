using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /*
    ToDo:
    Slime into trigger
    Implement spell system
    fireball
    
    */
    //player values
    public int speed = 25;
    public int jumpPower = 20;
    public bool canJump = true;
    public int hp = 5;
    //internal values
    public float freeze = 0;
    public float iFrame = 0;
    //identifiers
    public GameObject self;
    public Rigidbody2D rb;
    //items

    //spells: [plant, water, light, fire, ice, lightning] 
    public bool[] spells = new bool[6];
    //spell list
    public GameObject[] spellPrefabs = new GameObject[6];
    // fai, ???
    //controls
    public float horiz;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //cannot do anything if frozen
        if(freeze > 0){
            freeze -= Time.deltaTime;
        }else{
            if(freeze < 0){
                freeze = 0;
            }
            //iFrame tickdown only if not frozen
            if(iFrame > 0){
                iFrame -= Time.deltaTime;
            }else if(iFrame < 0){
                iFrame = 0;
            }
            //get the direction that you are pressing 
            horiz = 0;
            if(Input.GetKey("a")){
                horiz -=1;
            }
            if(Input.GetKey("d")){
                horiz += 1;
            }

            //changes the direction that you are facing if you are pressing right or left
            if(horiz > 0){
                this.transform.localScale = new Vector2(0.3f, 0.3f);
            }else if (horiz < 0){
                this.transform.localScale = new Vector2(-0.3f, 0.3f);
            }

            //Jump if you press up key
            if (Input.GetKey("space") && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                canJump = false;
            }
            //if not moving, or going in an opposite direction from the velocity
            if(horiz == 0 || (rb.velocity.x > 0 && horiz < 0) || (rb.velocity.y < 0 && horiz > 0)){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            //spell casting
            if(Input.GetMouseButtonDown(1) && spells[0]){
                Instantiate(spellPrefabs[0], transform.position + new Vector3(this.transform.localScale.x*2f, 0, 0), transform.rotation);
            }
            //move w/ horiz
            rb.velocity = new Vector2(horiz * speed, rb.velocity.y);
        }
        //enemies are pass throughable if you get hit
        if(iFrame > 0){
            Debug.Log(this.gameObject.GetComponent<BoxCollider2D>().excludeLayers);
        }
        //r key resets position
        if (Input.GetKey("r"))
        {
            rb.velocity = new Vector2(0, 0);
            rb.position = new Vector2(3, 1);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //touch grass !!!!!
        if (col.gameObject.CompareTag("floor"))
        {
            //resets jump when on the floor
            canJump = true;
        }
        //hit by an enemy
        if(col.gameObject.CompareTag("enemy") && iFrame <= 0){
            freeze = 0.2f;
            iFrame = 1f;
            if(col.gameObject.transform.position.x>self.transform.position.x){
                rb.velocity = new Vector2(-10, rb.velocity.y / 1.5f);
            }else{
                rb.velocity = new Vector2(10, (rb.velocity.y / 1.5f));
            }
        }
    }
    void OnCollisionStay2D(Collision2D col){
        
        if (col.gameObject.CompareTag("floor"))
        {
            //resets jump when on the floor
            canJump = true;
        } 
        
    }
    void OnCollisionExit2D(Collision2D col){

        if (col.gameObject.CompareTag("floor"))
        {
            //resets jump when on the floor
            canJump = false;
        }
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.CompareTag("enemy") && iFrame <= 0){
            freeze = 0.2f;
            iFrame = 1f;
            if(col.gameObject.transform.position.x>self.transform.position.x){
                rb.velocity = new Vector2(-10+col.gameObject.GetComponent<Rigidbody2D>().velocity.x, rb.velocity.y / 1.5f+2);
            }else{
                rb.velocity = new Vector2(10+col.gameObject.GetComponent<Rigidbody2D>().velocity.x, rb.velocity.y / 1.5f+2);
            }
    }
    void OnTriggerEnter2D(Collider2D col){
        
            Debug.Log(col.gameObject.name);
        if(col.gameObject.CompareTag("scroll")){
            spells[col.gameObject.GetComponent<scrollSC>().num] = true;
            Destroy(col.gameObject);
        }else if (col.gameObject.CompareTag("death"))
        {
            //death 
            Debug.Log(col.gameObject.name);
            //switch to triggers?
            rb.velocity = new Vector2(0, 0);
            freeze = 1;
            hp--;
            rb.position = new Vector2(3, 1);
        }
        }
    }
    void OnTriggerExit2D(Collider2D col){
        
            Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "bounds"){
            rb.velocity = new Vector2(0, 0);
            freeze = 0.5f;
            hp--;
            Debug.Log(col.gameObject.name);
            rb.position = new Vector2(3, 1);
        }
    }
    void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        // If it hits something...
        if (hit.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            Debug.Log(distance);
            
        }
    }

}
