/*  Filename:           Box.cs
 *  Author:             Atta Jirofty
 *  Last Update:        Jan 25, 2023
 *  Description:        Box script, with a self desctuct method
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 */

using UnityEngine;

/// <summary>
/// class <c>Box</c> is used to locate the box game object when finding game object using Unity function
/// </summary>

public class Box : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
