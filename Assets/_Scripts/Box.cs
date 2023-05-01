/*  Filename:           Box.cs
 *  Author:             Atta Jirofty
 *  Last Update:        Jan 25, 2023
 *  Description:        Box script, with a self desctuct method
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// class <c>Box</c> is used to locate the box game object when finding game object using Unity function
/// </summary>

public class Box : MonoBehaviour
{
    public void DestroySelf()
    {
       StartCoroutine(DelayInDestruction());
    }

    IEnumerator DelayInDestruction()
    {
        // To make a delay when destorying boxes, because of 0.5 secs of animation duration
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


}
