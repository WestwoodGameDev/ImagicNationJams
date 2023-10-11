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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //slime script
        if(type == "slime"){
            if(inRange&&cd<=0&&attack<0){
                attack = 0;
            }else if(inRange&&cd<=0){
                attack += Time.deltaTime;
            }
            if(attack>=0.66){
                cd = 5;
                rb.velocity = new Vector2(2, rb.velocity.y);
            }
        }
        if(rb.velocity.x)
        rb.velocity = new Vector2(rb.velocity.x-0.1f, rb.velocity.y);
    }
    public void onTriggerEnter(Collider col){
    
    }
    public void onTriggerStay(Collider col){
        if(col.gameObject.name == "player"){
            inRange = true;
        }
    }
    public void onTriggerExit(Collider col){
        if(col.gameObject.name == "player"){
            inRange = false;
        }
    }
}