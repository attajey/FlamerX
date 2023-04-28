/*  Filename:           SaveManager.cs
 *  Author:             Juan Munoz Rivera, Yuk Yee Wong
 *  Last Update:        December 8, 2022
 *  Description:        Save and load the player data
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 *                      December 7, 2022 (Yuk Yee Wong): Add a LoadGame static bool
 *                      December 8, 2022 (Yuk Yee Wong): Add a SerializePlayerData to be called from test script
 */

using UnityEngine;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;

/// <summary>
/// class <c>SaveManager</c> saves and loads the player data
/// </summary>

public static class SaveManager
{
    private static string dataPath = Application.persistentDataPath + "/savedata.json";
    public static bool LoadGame = false;

    /// <summary>
    /// Save Player Data
    /// </summary>
    /// <param name="playerBehaviour"></param>
    /// <param name="enemiesData"></param>
    /// <param name="boxData"></param>
    public static void SavePlayerData(TempPlayerController playerBehaviour, EnemyData[] enemiesData, BoxData[] boxData)
    {
        // Create new player data
        PlayerData playerData = new PlayerData(playerBehaviour, enemiesData, boxData, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // Save as a readable json file
        TextWriter writer = null;
        try
        {
            var contentsToWriteToFile = SerializePlayerData(playerData);
            writer = new StreamWriter(dataPath, false);
            writer.Write(contentsToWriteToFile);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    public static string SerializePlayerData(PlayerData playerData)
    {
        return JsonConvert.SerializeObject(
                playerData,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
                );
    }

    /// <summary>
    /// Load Player Data
    /// </summary>
    /// <returns></returns>
    public static PlayerData GetSavedPlayerData()
    {
        // Load json file if data exists
        if (File.Exists(dataPath))
        {
            // Create a new stream reader
            using StreamReader reader = new StreamReader(dataPath);

            // Read the json file
            string json = reader.ReadToEnd();

            // Convert json to player data
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            return playerData;
        }
        else
        {
            // Create a log warning if no data is found
            Debug.LogWarning("No data found");
            return null;
        }
    }
}
