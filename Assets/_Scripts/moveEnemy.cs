using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TS;

public class moveEnemy : MonoBehaviour
{
    public enum State { Patrol, Chase, Stop }
    public State ActiveState = State.Patrol;
    public enum Waypoint { Left, Right }
    public Waypoint ActiveWaypoint = Waypoint.Left;

    public GameObject Player;
    public GameObject A;
    public GameObject B;
    public GameObject goal;

    tagSensor sensor;

    public float speed = 20;
    public float range = 6;
    public float angle;

    public float estimate;

    void Awake()
    {
        sensor = new tagSensor(gameObject,0,range,angle);
    }

    void Start()
    {
        Debug.DrawRay(transform.position, (Quaternion.AngleAxis(-angle / 2, transform.up) * transform.forward) * range, Color.green);
    }
    
	void Update ()
    {
        float step = speed * Time.deltaTime;
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        estimate = estimateDotToAngle(transform.forward, Vector3.Normalize(Player.transform.position - transform.position)) * 2;

        switch (ActiveState) //
        {
            case State.Patrol:
                if(ActiveWaypoint == Waypoint.Left)
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
            if (dist <= range & angle > estimate)
            {
                ActiveState = State.Chase;
            }
            else if (dist > range)
            {
                ActiveState = State.Patrol;
            }
        }

        sensor.GridLines();
        
        if (transform.position == Player.transform.position)
        {
            Player.transform.position = new Vector3(0, 0, -145);
            transform.position = new Vector3(0, 0, -130);
            ActiveState = State.Patrol;
        }
    }

    static float estimateDotToAngle(Vector3 A, Vector3 B)
    {
        float angle = 90 * (-Vector3.Dot(A, B) + 1);
        return angle;
    }
}

