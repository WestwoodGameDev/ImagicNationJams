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
    //goofy
    public bool hax = false;
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
    /*
    *ideas
    *like a corrupted form of things (opposite of the color wheel/"high contrast mode")
     
     */
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
            Physics2D.IgnoreLayerCollision(9, 8, true);
        }else{
            if(freeze < 0){
                freeze = 0;
            }
            //iFrame tickdown only if not frozen
            if(iFrame > 0){
                Physics2D.IgnoreLayerCollision(9, 8, true);
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
            if (Input.GetKey("space") && (canJump||hax)) 
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                canJump = false;
            }
            //if not moving, or going in an opposite direction from the velocity
            if(horiz == 0 || (rb.velocity.x > 0 && horiz < 0) || (rb.velocity.y < 0 && horiz > 0)){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            //spell casting
            if(Input.GetMouseButtonDown(1) && (spells[0]||hax)){
                Instantiate(spellPrefabs[0], transform.position + new Vector3(this.transform.localScale.x*2f, 0, 0), transform.rotation);
            }
            //move w/ horiz
            rb.velocity = new Vector2(horiz * speed, rb.velocity.y);
        }
        //enemies are pass throughable if you get hit
        if(iFrame<=0){
            Physics2D.IgnoreLayerCollision(9,8,false);
        }
        //r key resets position
        if (hax&&Input.GetKey("r"))
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
            if(!hax){freeze = 0.2f;}
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
    //     if(col.gameObject.CompareTag("enemy") && iFrame <= 0){
    //         freeze = 0.2f;
    //         iFrame = 1f;
    //         if(col.gameObject.transform.position.x>self.transform.position.x){
    //             rb.velocity = new Vector2(-10+col.gameObject.GetComponent<Rigidbody2D>().velocity.x, rb.velocity.y / 1.5f+2);
    //         }else{
    //             rb.velocity = new Vector2(10+col.gameObject.GetComponent<Rigidbody2D>().velocity.x, rb.velocity.y / 1.5f+2);
    //         }
    // }
    }
    void OnTriggerEnter2D(Collider2D col){
        
        if(col.gameObject.CompareTag("scroll")){
            spells[col.gameObject.GetComponent<scrollSC>().num] = true;
            Destroy(col.gameObject);
        }
        // else if (col.gameObject.CompareTag("death"))
    //     {
    //         //death 
    //         //switch to triggers?
    //         rb.velocity = new Vector2(0, 0);
    //         freeze = 1;
    //         hp--;
    //         rb.position = new Vector2(3, 1);
    //     }
    }
    void OnTriggerExit2D(Collider2D col){
        
        if(col.gameObject.name == "bounds"&&!hax){
            rb.velocity = new Vector2(0, 0);
            freeze = 0.5f;
            hp--;
            rb.position = new Vector2(3, 1);
        }
    }
}