/*  Filename:           SelfDestroy.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 5, 2022
 *  Description:        Self destruction function to be called in the Unity inspector
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

/// <summary>
/// class <c>SelfDestroy</c> contains a self destruction function to be called in the Unity inspector
/// </summary>
public class SelfDestroy : MonoBehaviour
{
    public void SelfDestruction()
    {
        Destroy(gameObject);
    }
}
