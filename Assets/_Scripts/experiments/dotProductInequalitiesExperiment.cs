using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotProductInequalitiesExperiment : MonoBehaviour
{
    public float dotLeft;
    public float dotRight;

    public Vector3 dirLB;
    public Vector3 dirRB;
    public Vector3 dirC;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject compare;
    public GameObject mainObject;

    public void Start ()
    {
		
	}
	
	void Update ()
    {
        dirLB = Vector3.Normalize(leftBound.transform.position - mainObject.transform.position);
        dirRB = Vector3.Normalize(rightBound.transform.position - mainObject.transform.position);
        dirC = Vector3.Normalize(compare.transform.position - mainObject.transform.position);

        dotLeft = Vector3.Dot(dirC, dirLB);
        dotRight = Vector3.Dot(dirC, dirRB);

        drawLegs(leftBound);
        drawLegs(rightBound);
        Debug.DrawLine(mainObject.transform.position, compare.transform.position, Color.blue);
        Debug.DrawRay(mainObject.transform.position, transform.forward, Color.red);

        //Debug.Log("dirC:" + dirC + " dotLeft" + dotLeft + " dotRight" + dotRight);
	}

    void drawLegs(GameObject g)
    {
        Debug.DrawLine(mainObject.transform.position, g.transform.position, Color.green);
    }
}
