/*  Filename:           EnemyData.cs
 *  Author:             Juan Munoz Rivera, Yuk Yee Wong
 *  Last Update:        December 8, 2022
 *  Description:        Enemy data for recording the position, the waypoints, and the game object name
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 *                      December 8, 2022 (Yuk Yee Wong): Added more data to be saved for the MovingObject.
 */


using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// class <c>EnemyData</c> saves the position, the waypoints, and the game object name
/// </summary>
[System.Serializable]

public class EnemyData
{
    public string name;
    public Vector3 position;
    public Vector3 mainTransformPosition;
    public List<Vector3> waypointPositions;
    public int currentWaypoint;

    public EnemyData(MovingObject movingObject)
    {
        name = movingObject.gameObject.name;
        position = movingObject.transform.position;
        mainTransformPosition = movingObject.GetMovingTransformPosition();
        waypointPositions = movingObject.GetWaypointPositions();
        currentWaypoint = movingObject.CurrentWayPoint;
    }
}