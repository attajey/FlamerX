/*  Filename:           SceneManagers.cs
 *  Author:             Mohammadfarid Fakori, Yuk Yee Wong
 *  Last Update:        December 7, 2022
 *  Description:        Contains functions for UI buttons to use
 *  Revision History:   November 29, 2022 (Mohammadfarid Fakori): Initial script.
 *                      December 7, 2022 (Yuk Yee Wong): Cleaned the script, added the save game function, updated the load game function.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class <c>SceneManagers</c> contains functions for UI buttons to use
/// </summary>
public class SceneMenuManager : MonoBehaviour
{
    /// <summary>
    /// Used by the start button on the title screen (main menu scene) to load the first game level 
    /// </summary>
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Used by the save button on the pause screen
    /// </summary>
    public void SaveGame()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.SaveGame();
        }
    }

    /// <summary>
    /// Used by the load button on the title screen (main menu scene) and the pause screen
    /// </summary>
    public void LoadGame()
    {
        PlayerData playerData = SaveManager.GetSavedPlayerData();
        if (playerData != null && !string.IsNullOrEmpty(playerData.sceneName))
        {
            SaveManager.LoadGame = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(playerData.sceneName);
        }
    }

    /// <summary>
    /// Used by the exit button on the title screen (main menu scene)
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}
