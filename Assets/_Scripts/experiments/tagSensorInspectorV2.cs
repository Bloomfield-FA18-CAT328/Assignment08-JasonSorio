using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TS;

public class tagSensorInspectorV2 : MonoBehaviour
{
    TagSensor sensorA;
    TagSensor sensorB;
    public TagSensorStructV2[] TagSensorStructs;

    public string name;
    public string tagName; //do not leave Tag Name empty before you play
    public float maxRange;
    public float minRange;
    public float FOV;
    public float lookAtAngle;
    public bool Detected;

    void Awake()
    {
        sensorA = new TagSensor(gameObject, minRange, maxRange, FOV, lookAtAngle);
    }

    void Start()
    {

    }

    void Update()
    {
        //without structs
        sensorA.MaxRange = maxRange;
        sensorA.MinRange = minRange;
        sensorA.FOV = FOV;
        sensorA.OffsetY = lookAtAngle;
        Detected = sensorA.OnDetect(tagName);
        sensorA.GridLines();
    }
}

[System.Serializable]
public struct TagSensorStructV2
{
    public bool Disabled;
    public string name;
    public string tagName; //do not leave Tag Name empty before you play
    public float maxRange;
    public float minRange;
    public float FOV;
    public float lookAtAngle;
    public bool Detected;
}
