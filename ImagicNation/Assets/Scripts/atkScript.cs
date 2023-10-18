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
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("player").GetComponent<Transform>().localScale.x<0){
            direction = 1;
        }else{
            direction = -1;
        }
        this.transform.localScale = new Vector2(0.4f*direction,0.4f);
        
    }
    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector2.left*direction*Time.deltaTime*10);
       if(!start){
        clock += Time.deltaTime;
        if(clock>=0.1){
            start = true;
        }
       }
    }
    void OnCollisionEnter2D(Collision2D col){
        if((col.gameObject.name != "room" && col.gameObject.name != "player")||(start&&col.gameObject.name == "player"))
        {        
            Debug.Log(col.gameObject.name);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.name != "bounds"){
            
            Debug.Log(col.gameObject.name);
            Destroy(gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.name == "bounds"){
            Debug.Log(col.gameObject.name);
            Destroy(gameObject);
        }
    }
}
