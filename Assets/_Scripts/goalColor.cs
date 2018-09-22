using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalColor : MonoBehaviour {

    public GameObject player;
    public Color myColor = Color.green;
    public Renderer rend;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z >= transform.position.z)
        {
            rend.material.color = myColor;
        }
        else
        { 
            rend.material.color = Color.white;
        }
    }
}
