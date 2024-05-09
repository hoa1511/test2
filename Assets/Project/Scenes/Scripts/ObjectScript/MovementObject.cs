using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour, IMovement
{
    [SerializeField] private GameObject[] waypoints;
    public float speed = 1f;


    private int currentWaypointIndex = 0;

    private void Update() 
    {
        Move(); 
    }

    public virtual void Move()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.unscaledDeltaTime);
    }
}
