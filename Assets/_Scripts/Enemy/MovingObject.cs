/*  Filename:           MovingObject.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 8, 2022
 *  Description:        Controls the game object movment using waypoints, speed, animator, and offset
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 *                      December 8, 2022 (Yuk Yee Wong): Added functions to retrieve data and setup
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class <c>MovingObject</c> controls the game object movment using waypoints, speed, animator, and offset
/// </summary>
public class MovingObject : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float offset = 0.01f;
    [SerializeField] private Transform movingTransform;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private Animator animator;
    [SerializeField] private string walkingParameterName = "xVelocity";

    [Header("Debug")]
    [SerializeField] private int currentWayPoint;
    [SerializeField] private bool hasRigidBody;

    public float Speed { get => speed; set => speed = value; }
    public int CurrentWayPoint { get { return currentWayPoint; } }

    public Vector3 GetMovingTransformPosition()
    {
        return movingTransform.position;
    }

    public List<Vector3> GetWaypointPositions()
    {
        List<Vector3> waypointPositions = new List<Vector3>();
        foreach (Transform waypoint in waypoints.Points)
        {
            waypointPositions.Add(waypoint.transform.position);
        }
        return waypointPositions;
    }

    private void Awake()
    {
        // Assign the moving transform
        waypoints.AssignMovingTransform(movingTransform);

        // Update the hasRigidBody boolean
        hasRigidBody = movingTransform.GetComponent<Rigidbody2D>() != null;
    }

    private void Update()
    {
        // Move the moving transform between waypoints
        Move();
    }

    public void Move()
    {
        // Check if at least one waypoints exist
        if (waypoints != null && waypoints.Points.Count > 0 && waypoints.Points.Count > currentWayPoint)
        {
            // Move the transform if the transform does not reach the waypoint
            if (!IsWaypointReached())
            {
                // Update y position of the destination position if the transform has rigidbody
                Vector2 destination = waypoints.Points[currentWayPoint].position;
                if (hasRigidBody)
                {
                    destination.y = transform.position.y;
                }

                // Move the moving transform according to the destination and the speed
                movingTransform.position = Vector3.MoveTowards(movingTransform.position, destination, speed * Time.deltaTime);

                // Set the walking parameter if animator exists
                if (animator != null)
                {
                    animator.SetFloat(walkingParameterName, 1f);
                }

                // Flip the moving transform according to the moving direction
                if (movingTransform.position.x < destination.x && movingTransform.localScale.x < 0)
                {
                    movingTransform.localScale = new Vector3(Mathf.Abs(movingTransform.localScale.x), movingTransform.localScale.y, movingTransform.localScale.z);
                }
                else if (movingTransform.position.x > destination.x && movingTransform.localScale.x > 0)
                {
                    movingTransform.localScale = new Vector3(-movingTransform.localScale.x, movingTransform.localScale.y, movingTransform.localScale.z);
                }
            }
            // Update the current waypoint number when reaching the waypoint
            else
            {
                currentWayPoint = currentWayPoint + 1 >= waypoints.Points.Count ? 0 : currentWayPoint + 1;
            }
        }
    }

    private bool IsWaypointReached()
    {
        return Vector3.Distance(movingTransform.position, waypoints.Points[currentWayPoint].position) < offset;
    }

    /// <summary>
    /// Set up the moving object
    /// </summary>
    /// <param name="movingTransformPosition"></param>
    /// <param name="waypointPositions"></param>
    /// <param name="currentWayPoint"></param>
    public void Setup(Vector3 position, Vector3 movingTransformPosition, List<Vector3> waypointPositions, int currentWayPoint)
    {
        transform.position = position;
        movingTransform.position = movingTransformPosition;
        waypoints.ReassigneWaypointPositions(waypointPositions);
        this.currentWayPoint = currentWayPoint;
    }
}
