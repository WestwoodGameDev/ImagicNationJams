using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkScript : MonoBehaviour
{
    public string type;
    public int direction = 1;
    public float temp;
    public bool start;
    public float clock;

    public bool done;

    public Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("player").GetComponent<Transform>().localScale.x < 0){
            direction = 1;
        }else{
            direction = -1;
        }
        this.transform.localScale = new Vector2(0.4f * direction, 0.4f);
        rb.velocity = new Vector2(-direction * 20, 0);
    }
    // Update is called once per frame
    void Update()
    {
    //    transform.Translate(Vector2.left*direction*Time.deltaTime*10);
       if(!start){
        clock += Time.deltaTime;
        if(clock >= 0.1){
            start = true;
        }
       }
       if(done){
            rb.velocity = new Vector2(0,0);
       }
    }
    void OnCollisionEnter2D(Collision2D col){
        if((col.gameObject.name != "room" && col.gameObject.name != "player") || (start && col.gameObject.name == "player"))
        {        
                anim.SetBool("explode", true);
                done = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.name != "bounds" && col.gameObject.name != "player"){
                anim.SetBool("explode", true);
                done = true;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.name == "bounds"){
            Destroy(gameObject);
        }
    }
    void die(){ Destroy(gameObject); }
}
