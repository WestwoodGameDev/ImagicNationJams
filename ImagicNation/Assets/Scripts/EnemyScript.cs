using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator anim;
    public GameObject self;   
    public Rigidbody2D rb;
    public string type;
    public bool inRange = false;
    public float attack = 1;
    public float cd = 0;
    public float atkVelo = 10;
    public int atkDir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //slime script
        if(type == "slime"){
            if(cd <= 0 && inRange){
                anim.SetBool("attack", true);
                if(GameObject.Find("player").GetComponent<Transform>().position.x > this.transform.position.x){
                    atkDir = 1;
                }else{
                    atkDir = -1;
                }
                this.transform.localScale = new Vector2(0.2f * atkDir, 0.2f);
                cd = 2.5f;
            }else{
                cd -= Time.deltaTime;
            }
            
        }
        //slow down
        if(rb.velocity.x > 0.1f){
            rb.velocity = new Vector2(rb.velocity.x - 10 * Time.deltaTime, rb.velocity.y) ;
        }else if(rb.velocity.x<-0.1f){
            rb.velocity = new Vector2(rb.velocity.x + 10 * Time.deltaTime, rb.velocity.y);
        }else if(rb.velocity.x != 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
//controled by anim
    public void zlimeAttack(float time){
        if(time <= 0){
            attack = 0;
        }else{
            anim.SetBool("attack", false);
            rb.velocity = new Vector2(atkVelo * atkDir, rb.velocity.y);
        }
    }
    public void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("attack")){
            if(col.gameObject.GetComponent<atkScript>().type == "fire"){
                rb.velocity = new Vector2(col.gameObject.GetComponent<atkScript>().direction*-7.5f, rb.velocity.y);
            }

        }
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "death"){
            Destroy(gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.name == "player"){
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.name == "player"){
            inRange = false;
        }
    }

}
