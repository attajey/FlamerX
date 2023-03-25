/*  Filename:           Waypoints.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 8, 2022
 *  Description:        Used by game designer to create and update waypoints in the Unity inspector
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 *                      December 8, 2022 (Yuk Yee Wong): Created reassign waypoint position function which is used after loading
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class <c>Waypoints</c> is used by game designer to create and update waypoints in the Unity inspector
/// </summary>
public class Waypoints : MonoBehaviour
{
    public Transform waypointPrefab; // accessed by WaypointsEditor.cs
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Transform mainTransform;

    public List<Transform> Points { get => waypoints; set => waypoints = value; }

    private void Awake()
    {
        waypoints = new List<Transform>();
        UpdateWaypoints();
    }

    /// <summary>
    /// Reassign waypoint positions after instantiating
    /// </summary>
    /// <param name="positions"></param>
    public void ReassigneWaypointPositions(List<Vector3> positions)
    {
        if (waypoints.Count != positions.Count)
        {
            return;
        }

        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i].transform.position = positions[i];
        }
    }

    /// <summary>
    /// Assign the moving transform to draw the gizmo between waypoints when the main transform is not null
    /// </summary>
    /// <param name="movingTransform"></param>
    public void AssignMovingTransform(Transform movingTransform)
    {
        mainTransform = movingTransform;
    }

    /// <summary>
    /// Remove Waypoint function used by WaypointsEditor.cs
    /// </summary>
    /// <param name="index"></param>
    public void RemoveWaypoint(int index)
    {
        DestroyImmediate(transform.GetChild(index).gameObject);
        Points.RemoveAt(index);
        Points.TrimExcess();
        UpdateWaypoints();
    }

    /// <summary>
    /// Add new waypoint function used by WaypointsEditor.cs
    /// </summary>
    public void AddNewWaypoint()
    {
        Transform waypoint = Instantiate(waypointPrefab, transform);
        waypoint.name = string.Format("Waypoint {0}", waypoints.Count + 1);
        UpdateWaypoints();
    }

    /// <summary>
    /// Update waypoints function used by WaypointsEditor.cs
    /// </summary>
    public void UpdateWaypoints()
    {
        waypoints.Clear();

        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
    }

    /// <summary>
    /// Clear waypoints function used by WaypointsEditor.cs
    /// </summary>
    public void ClearWaypoints()
    {
        waypoints.Clear();

        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    /// <summary>
    /// Draw the gizmos between waypoints if the main transform is not null and no of waypoints is more than one
    /// </summary>
    private void OnDrawGizmos()
    {
        if (mainTransform != null && waypoints != null && waypoints.Count > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (i + 1 == Points.Count)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                else
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
