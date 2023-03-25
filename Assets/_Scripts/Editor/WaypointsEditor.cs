/*  Filename:           WaypointsEditor.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 5, 2022
 *  Description:        Editor script for Waypoints.cs
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor script for Waypoints.cs to aid designer to add, remove, update the waypoint list
/// </summary>
[CustomEditor(typeof(Waypoints))]
public class WaypointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Waypoints waypoints = (Waypoints)target;

        waypoints.waypointPrefab = (Transform)EditorGUILayout.ObjectField("Waypoint prefab", waypoints.waypointPrefab, typeof(Transform), false);

        EditorGUILayout.LabelField("Waypoints: ", EditorStyles.boldLabel);

        if (waypoints.Points != null && waypoints.Points.Count > 0)
        {
            for (int i = 0; i < waypoints.Points.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                waypoints.Points[i].name = EditorGUILayout.TextField(waypoints.Points[i].name);
                waypoints.Points[i].position = EditorGUILayout.Vector2Field("", waypoints.Points[i].position);
                if (GUILayout.Button("Delete"))
                {
                    waypoints.RemoveWaypoint(i);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        if (GUILayout.Button("Add Waypoint"))
        {
            waypoints.AddNewWaypoint();
        }

        if (GUILayout.Button("Update Waypoints"))
        {
            waypoints.UpdateWaypoints();
        }

        if (GUILayout.Button("Clear All Waypoints"))
        {
            waypoints.ClearWaypoints();
        }

        // base.OnInspectorGUI();
    }
}
