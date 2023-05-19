using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoint positions
    public float speed = 5f; // Speed of the AI car
    public float turnSpeed = 5f; // Turning speed of the AI car
    private int currentWaypointIndex = 0; // Index of the current waypoint

    private void Start()
    {
        // Initialize the current waypoint to the first one
        if (waypoints.Length > 0)
            transform.position = waypoints[0].position;
    }

    private void Update()
    {
        if (waypoints.Length == 0)
            return;

        // Calculate the direction to the current waypoint
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
        direction.Normalize();

        // Calculate the target rotation based on the direction to the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate the AI car towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Move the AI car forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the AI car has reached the current waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);
        if (distanceToWaypoint < 0.5f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}