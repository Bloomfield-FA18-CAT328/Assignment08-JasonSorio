using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
        }
        
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 20);
        }
        
    }
}
