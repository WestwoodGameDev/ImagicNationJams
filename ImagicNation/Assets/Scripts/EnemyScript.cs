using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject self;   
    public Rigidbody2D rb;
    public string type;
    public bool inRange = false;
    public float attack = 0;
    public float cd = 0;
    public float atkVelo = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //slime script
        if(type == "slime"){
            if(cd<=0&&inRange){
                if(attack>=0.66){
                    attack = 0;
                }else{

                attack += Time.deltaTime;
                }
                if(attack>=0.66){
                    cd = 5;
                    rb.velocity = new Vector2(atkVelo, rb.velocity.y);
                }
            }else{
                cd -= Time.deltaTime;
            }
            
        }
        if(rb.velocity.x>0.1f){
            rb.velocity = new Vector2(rb.velocity.x-10*Time.deltaTime, rb.velocity.y) ;
        }else if(rb.velocity.x<-0.1f){
            rb.velocity = new Vector2(rb.velocity.x+10*Time.deltaTime, rb.velocity.y);
        }else if(rb.velocity.x != 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    public void OnTriggerEnter2D(Collider2D col){

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