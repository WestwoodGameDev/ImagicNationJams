using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(cam.orthographicSize);
        this.transform.position = player.transform.position+ new Vector3(0, 0, -5);
    }
}
