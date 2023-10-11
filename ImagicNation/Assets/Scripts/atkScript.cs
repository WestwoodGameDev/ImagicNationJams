using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkScript : MonoBehaviour
{
    public string type;
    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector2.left*direction*Time.deltaTime*10);
 
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name != "room"&&col.gameObject.name !="player"){
            Destroy(gameObject);
        }
    }
}
