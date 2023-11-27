using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrierSC : MonoBehaviour
{
    float deathTimer = 2f;
    bool go = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(go){
            deathTimer-=Time.deltaTime;
        }
        if(deathTimer<=0){
            Destroy(gameObject);
        }
    }

    void onTriggerEnter(Collider2D col){
        if(col.gameObject.CompareTag("attack")){
            if(col.gameObject.GetComponent<atkScript>().type == "fire"){
                go = true;
            }
        }
    }
}
