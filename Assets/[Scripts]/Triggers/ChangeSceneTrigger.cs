/*  Filename:           ChangeSceneTrigger.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 5, 2022
 *  Description:        A trigger to load a scene
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class <c>ChangeSceneTrigger</c> is used to change scene when the player triggers
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ChangeSceneTrigger: MonoBehaviour
{
    [Header("Load Scene")]
    [SerializeField] private string sceneLevel;

    /// <summary>
    /// Load a scene when the player triggers
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the player triggers, load a scene
        if (IsPlayer(other.gameObject))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneLevel);
        }
    }

    private bool IsPlayer(GameObject other)
    {
        return other.CompareTag("Player");
    }
}
