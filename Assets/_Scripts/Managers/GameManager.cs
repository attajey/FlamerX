/*  Filename:           GameManager.cs
 *  Author:             Juan Munoz Rivera, Atta Jirofty, Yuk Yee Wong
 *  Last Update:        December 8, 2022
 *  Description:        Game Manager to help retrieving the game scene data and update the game scene according to the loaded data
 *  Revision History:   November 26, 2022 (Juan Munoz Rivera): Initial script.
 *                      November 30, 2022 (Atta Jirofty): Added load function.
 *                      December 8, 2022 (Yuk Yee Wong): Reorganised the code and updated the InstantiateEnemies function.
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager to help retrieving the game scene data and update the game scene according to the loaded data
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject boxPrefab;
    public GameObject playerPrefab;

    private void Start()
    {
        if (SaveManager.LoadGame)
        {
            SaveManager.LoadGame = false;

            // Load player data from save manager
            PlayerData loadedPlayerData = SaveManager.GetSavedPlayerData();

            if (loadedPlayerData != null)
            { 
                // Load player, enemeis, and boxes
                LoadGame(loadedPlayerData);
            }
        }
    }

    private void LoadGame(PlayerData playerData)
    {
        DestroyAllEnemies();
        DestroyAllBoxes();
        DestroyPlayer();

        InstantiatePlayer(playerData);
        InstantiateEnemies(playerData);
        InstantiateBoxes(playerData);
    }

    private void DestroyAllEnemies()
    {
        // Find enemies game objects from the scene
        GameObject enemies = GameObject.Find("Enemies"); 
        
        foreach (Transform enemy in enemies.transform)
        {
            Destroy(enemy.gameObject);
        }
    }

    private void DestroyAllBoxes()
    {
        // Find boxes game objects from the scene
        GameObject boxes = GameObject.Find("Boxes");

        foreach (Transform box in boxes.transform)
        {
            Destroy(box.gameObject);
        }
    }

    private void DestroyPlayer()
    {
        // Find player game object from the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
    }

    /// <summary>
    /// Instantiate player from the player data
    /// </summary>
    /// <param name="playerData"></param>
    private void InstantiatePlayer(PlayerData playerData)
    {
        // Instantaite player
        GameObject newPlayer = Instantiate(
                playerPrefab,
                new Vector3(playerData.position[0], playerData.position[1], 0),
                Quaternion.identity
                );

        // Set up player camera
        Cinemachine.CinemachineVirtualCamera vc = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        vc.Follow = newPlayer.transform;
    }

    /// <summary>
    /// Instantiate enemies from the player data
    /// </summary>
    /// <param name="playerData"></param>
    private void InstantiateEnemies(PlayerData playerData)
    {
        // Find enemies game objects from the scene
        GameObject enemies = GameObject.Find("Enemies");

        foreach (var enemy in playerData.enemiesData)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.parent = enemies.transform;
            MovingObject movingObject = newEnemy.GetComponent<MovingObject>();
            movingObject.Setup(enemy.position, enemy.mainTransformPosition, enemy.waypointPositions, enemy.currentWaypoint);
        }
    }

    /// <summary>
    /// Instantiate boxes from the player data
    /// </summary>
    /// <param name="playerData"></param>
    private void InstantiateBoxes(PlayerData playerData)
    {
        // Find boxes game objects from the scene
        GameObject boxes = GameObject.Find("Boxes");

        foreach (var box in playerData.boxData)
        {
            var newBox = Instantiate(
                boxPrefab,
                new Vector3(box.position[0], box.position[1], 0),
                Quaternion.identity
                );

            newBox.transform.parent = boxes.transform;
        }
    }

    /// <summary>
    /// Save game logic called by the scene menu manager
    /// </summary>
    public void SaveGame()
    {
        // Get the player controller from the scene
        TempPlayerController playerController = GameObject.FindObjectOfType<TempPlayerController>();

        // Save player data
        SaveManager.SavePlayerData(playerController, GetEnemyDataList().ToArray(), GetBoxDataList().ToArray());
    }

    /// <summary>
    /// Get enemy data list from the game scene
    /// </summary>
    /// <remarks>Has to be publicly accessible by the test script to compare the loaded data</remarks>
    /// <returns></returns>
    public List<EnemyData> GetEnemyDataList()
    {
        // Find enemies game objects from the scene
        GameObject enemies = GameObject.Find("Enemies");

        // Create a new enemy data list
        List<EnemyData> enemiesData = new List<EnemyData>();

        // Update the enemy data
        foreach (Transform enemy in enemies.transform)
        {
            if (enemy.gameObject.activeSelf)
            {
                MovingObject tempMovingObject = enemy.gameObject.GetComponent<MovingObject>();
                EnemyData tempEnemyData = new EnemyData(tempMovingObject);
                enemiesData.Add(tempEnemyData);
            }
        }

        return enemiesData;
    }

    /// <summary>
    /// Get box data list from the game scene
    /// </summary>
    /// <remarks>Has to be publicly accessible by the test script to compare the loaded data</remarks>
    /// <returns></returns>
    public List<BoxData> GetBoxDataList()
    {
        // Find boxes game objects from the scene
        GameObject boxes = GameObject.Find("Boxes");

        // Create a new box data list
        List<BoxData> boxesData = new List<BoxData>();

        // Update the box data
        foreach (Transform box in boxes.transform)
        {
            if (box.gameObject.activeSelf)
            {
                Box tempBox = box.gameObject.GetComponent<Box>();
                BoxData tempBoxData = new BoxData(tempBox);
                boxesData.Add(tempBoxData);
            }
        }

        return boxesData;
    }
}
