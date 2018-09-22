using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TS;

public class testTagSensor : MonoBehaviour
{
    public enum mode { Sweep, Line, SweepLine, Grid }
    public mode Mode;

    tagSensor front;
    tagSensor back;
    tagSensor custom;

    public float min;
    public float max;
    public float fov;
    public float direction;
    public float SweepingRate;
    public bool detected;

    void Update ()
    {
        custom = new tagSensor(gameObject, min, max, fov, direction);

        switch (Mode)
        {
            case mode.Line:
                custom.DrawLines();
                break;

            case mode.Grid:
                custom.GridLines();
                break;

            case mode.Sweep:
                custom.SweepLines(SweepingRate);
                break;

            case mode.SweepLine:
                custom.SweepLines(SweepingRate/2);
                custom.DrawLines();
                break;

            default:
                break;
        }

        detected = custom.OnDetect("tag1");
        //Debug.Log(detected);
    }
}
