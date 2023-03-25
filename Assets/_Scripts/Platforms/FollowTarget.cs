/*  Filename:           FollowTarget.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 5, 2022
 *  Description:        Assign to the gameobject that needs to follow a target transform position
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

/// <summary>
/// class <c>FollowTarget</c> is used to follow a target transform position
/// <example>The "enemy" UI dialogue should move with the werefox tranform</example>
/// </summary>
public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        transform.position = target.position;
    }
}
