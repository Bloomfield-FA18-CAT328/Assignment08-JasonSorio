using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TS;

public class MoveEnemyV2 : MonoBehaviour
{
    public enum State { Patrol, Chase, Stop }
    public State ActiveState = State.Patrol;
    public enum Waypoint { Left, Right }
    public Waypoint ActiveWaypoint = Waypoint.Left;

    public GameObject Player;
    public GameObject A;
    public GameObject B;
    public GameObject goal;

    TagSensor sensor;

    public float speed;
    public float maxRange;
    public float minRange;
    public float FOV;
    public float lookAtAngle;
    public bool Detected;

    void Awake()
    {
        sensor = new TagSensor(gameObject, minRange, maxRange, FOV, lookAtAngle);
    }

    void Start()
    {
       
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        sensor.MaxRange = maxRange;
        sensor.MinRange = minRange;
        sensor.FOV = FOV;
        sensor.OffsetY = lookAtAngle;
        Detected = sensor.OnDetect("player");

        switch (ActiveState)
        {
            case State.Patrol:
                if (ActiveWaypoint == Waypoint.Left)
                {
                    transform.LookAt(A.transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, A.transform.position, step);
                }
                else if (ActiveWaypoint == Waypoint.Right)
                {
                    transform.LookAt(B.transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, B.transform.position, step);
                }

                if (transform.position == A.transform.position)
                {
                    ActiveWaypoint = Waypoint.Right;
                }
                else if (transform.position == B.transform.position)
                {
                    ActiveWaypoint = Waypoint.Left;
                }
                break;

            case State.Chase:
                transform.LookAt(Player.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
                break;

            case State.Stop:
                break;

            default:
                break;
        }

        if (Player.transform.position.z >= goal.transform.position.z)
        {
            ActiveState = State.Stop;
        }
        else
        {
            if (Detected)
            {
                ActiveState = State.Chase;
            }
            else
            {
                ActiveState = State.Patrol;
            }
        }

        sensor.GridLines();

        //if 
        if (transform.position == Player.transform.position)
        {
            Player.transform.position = new Vector3(0, 0, -145);
            transform.position = new Vector3(0, 0, -130);
            ActiveState = State.Patrol;
        }
    }
}

