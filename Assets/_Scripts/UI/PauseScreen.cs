/*  Filename:           PauseScreen.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 7, 2022
 *  Description:        Pause Screen called by escape key
 *  Revision History:   December 7, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

/// <summary>
/// class <c>PauseScreen</c> that can be called by escape key
/// </summary>
public class PauseScreen : MonoBehaviour
{
    // The pause screen canvas which is used in pause and resume function
    [SerializeField] private GameObject panel;

    private void Update()
    {
        // Pause and unpause the game when pressing escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrUnpauseGame();
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Called when pressing escape key
    /// </summary>
    private void PauseOrUnpauseGame()
    {
        if (Time.timeScale == 0)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Used by resume button on the pause screen
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    /// <summary>
    /// Used by pause button on the pause screen
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
