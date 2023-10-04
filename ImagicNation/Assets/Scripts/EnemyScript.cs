using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public string type;
    public GameObject target;
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //slime script
        if(type == "slime"){
            if(inRange){

            }
        }
    }
    public void onTriggerEnter(Collider col){
    
    }
    public void onTriggerStay(Collider col){
        if(col.gameObject.name == target.name){
            inRange = true;
        }
    }
    public void onTriggerExit(Collider col){
        inRange = false;
    }
}