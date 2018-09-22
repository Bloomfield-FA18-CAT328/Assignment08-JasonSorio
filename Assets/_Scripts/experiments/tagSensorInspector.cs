using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagSensorInspector : MonoBehaviour
{
    public MyStruct[] tags;

    public bool onDetect(string t)
    {
        for (int i = 0; i < tags.Length; i++)
            if (tags[i].tag == t)
                if (/*some method that uses angle and range*/ 1==1)
                    return true;

        return false;
    }
}

[System.Serializable]
public struct MyStruct
{
    public string tag;
    public int range;
    public int angle;
}
