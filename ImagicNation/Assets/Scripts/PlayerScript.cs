using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int speed = 25;
    public int jumpPower = 20;
    public bool canJump = true;
    public int hp = 5;
    public float freeze = 0;
    public GameObject self;
    public Rigidbody2D rb;
    public float horiz;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(freeze>0){
            freeze-=Time.deltaTime;
        }else{
            if(freeze<0){
                freeze = 0;
            }
            horiz = Input.GetAxis("Horizontal");
            if (Input.GetKey("up") && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                canJump = false;
            }
            if(horiz == 0){
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
            rb.velocity = new Vector2(horiz*speed, rb.velocity.y);
        }
        
        //r key resets position
        if (Input.GetKey("r"))
        {
            rb.velocity = new Vector2(0,0);
            rb.position = new Vector2(3,1);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("floor"))
        {
            //resets jump when on the floor
            canJump = true;
        }
        else if (col.gameObject.CompareTag("death"))
        {
            //death 
            //switch to triggers?
            rb.velocity = new Vector2(0,0);
            freeze = 1;
            hp--;
            rb.position = new Vector2(3, 1);
        }else if(col.gameObject.CompareTag("enemy")){
            freeze = 0.2f;
            if(col.gameObject.transform.position.x>self.transform.position.x){
                rb.velocity = new Vector2(-10,0);
            }else{
                rb.velocity = new Vector2(10,0);
            }
        }
    }
    void onCollisionStay2D(Collision2D col){
        
        if (col.gameObject.CompareTag("floor"))
        {
            //resets jump when on the floor
            canJump = true;
        }
    }
    void onCollisionExit2D(Collision2D col){

        if (col.gameObject.CompareTag("floor"))
        {
            //resets jump when on the floor
            canJump = false;
        }
    }
}
