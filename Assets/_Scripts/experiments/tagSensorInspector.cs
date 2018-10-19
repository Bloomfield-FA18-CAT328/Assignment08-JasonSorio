using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TS;

public class TagSensorInspector : MonoBehaviour
{
    TagSensor sensorA;
    TagSensor sensorB;
    public TagSensorStruct[] TagSensorStructs;

    public string name;
    public string tagName; //do not leave Tag Name empty before you play
    public float maxRange;
    public float minRange;
    public float FOV;
    public float lookAtAngle;
    public bool Detected;

    void Awake()
    {
        //one tag sensor
        sensorA = new TagSensor(gameObject, minRange, maxRange, FOV, lookAtAngle);

        //many tag sensors using structs
        for (int i = 0; i < TagSensorStructs.Length; i++) //  for each file
        {
            TagSensorStructs[i].Sensor = new TagSensor(gameObject, minRange, maxRange, FOV, lookAtAngle);
        }
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

        //with structs
        for (int i = 0; i < TagSensorStructs.Length; i++)
        {
            TagSensorStructs[i].Sensor.Disabled = TagSensorStructs[i].Disabled;
            TagSensorStructs[i].Sensor.MaxRange = TagSensorStructs[i].maxRange;
            TagSensorStructs[i].Sensor.MinRange = TagSensorStructs[i].minRange;
            TagSensorStructs[i].Sensor.FOV = TagSensorStructs[i].FOV;
            TagSensorStructs[i].Sensor.OffsetY = TagSensorStructs[i].lookAtAngle;
            TagSensorStructs[i].Detected = TagSensorStructs[i].Sensor.OnDetect(TagSensorStructs[i].tagName); //broken
            TagSensorStructs[i].Sensor.GridLines();
            //Debug.Log(TagSensorStructs[i].sensor.OnDetect(tagName));
        }
    }
}

[System.Serializable]
public struct TagSensorStruct 
{
    public TagSensor Sensor;
    public bool Disabled;
    public string name;
    public string tagName; //do not leave Tag Name empty before you play
    public float maxRange;
    public float minRange;
    public float FOV;
    public float lookAtAngle;
    public bool Detected;
}
