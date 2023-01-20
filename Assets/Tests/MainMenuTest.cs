/*  Filename:           MainMenuTest.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 8, 2022
 *  Description:        Unit test suite for main menu functions
 *  Revision History:   December 8, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools.Utils;

namespace PlayModeTests
{
    /// <summary>
    /// Unit test suite for main menu options button, play button, and load button
    /// </summary>
    public class MainMenuTest
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Load the main menu scene
            SceneManager.LoadScene(0);
            yield return new WaitForSeconds(2f);
        }

        /// <summary>
        /// Test options button
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestOptionsButton()
        {
            // Get the options screen by tag
            var optionsScreenObject = GameObject.FindWithTag("OptionsScreen");

            // Ensure there is not active options screen object exists
            Assert.IsNull(optionsScreenObject);

            // Get the options button by tag
            var optionsButtonObject = GameObject.FindWithTag("OptionsButton");

            // Ensure the options button object exists
            Assert.NotNull(optionsButtonObject);

            // Simulate options button click
            Button optionsButton = optionsButtonObject.GetComponent<Button>();
            optionsButton.onClick.Invoke();

            // Wait for the options screen to enable
            yield return new WaitForSecondsRealtime(2f);

            // Get the options screen by tag again
            optionsScreenObject = GameObject.FindWithTag("OptionsScreen");

            // Ensure the options screen is enabled
            Assert.IsTrue(optionsScreenObject.activeInHierarchy);
        }

        /// <summary>
        /// Test play button
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestPlayButton()
        {
            // Get the current scene information
            string currentSceneName = SceneManager.GetActiveScene().name;
            int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

            // Ensure the current scene has build index 0
            Assert.AreEqual(currentSceneBuildIndex, 0);

            // Get the play button by tag
            var playButtonObject = GameObject.FindWithTag("PlayButton");

            // Ensure the play button object exists
            Assert.NotNull(playButtonObject);

            // Simulate play button click
            Button playButton = playButtonObject.GetComponent<Button>();
            playButton.onClick.Invoke();

            // Wait for new scene to load
            yield return new WaitForSecondsRealtime(2f);

            // Get the loaded scene information
            string loadedSceneName = SceneManager.GetActiveScene().name;
            int loadedSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

            // Ensure the loaded scene and previous scene are not the same
            Assert.AreNotEqual(currentSceneName, loadedSceneName);

            // Ensure the loaded scene has build index 1
            Assert.AreEqual(loadedSceneBuildIndex, 1);
        }

        /// <summary>
        /// Test load logic
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestLoadButton()
        {
            // Get the load button by tag
            var loadButtonObject = GameObject.FindWithTag("LoadButton");

            // Ensure the load button object exists
            Assert.NotNull(loadButtonObject);

            // Simulate load button click
            Button loadButton = loadButtonObject.GetComponent<Button>();
            loadButton.onClick.Invoke();

            // Wait for new scene to load
            yield return new WaitForSecondsRealtime(2f);

            // Get the saved data
            PlayerData loadedPlayerData = SaveManager.GetSavedPlayerData();

            var contentsToWriteToFile = SaveManager.SerializePlayerData(loadedPlayerData);
            Debug.Log(contentsToWriteToFile);

            if (loadedPlayerData != null)
            { 
                // Get the loaded scene information
                string loadedSceneName = SceneManager.GetActiveScene().name;

                Debug.Log($"{loadedSceneName} {loadedPlayerData.sceneName}");
                // Ensure the same scene is loaded
                Assert.AreEqual(loadedSceneName, loadedPlayerData.sceneName);

                // Get player object by tag
                var playerObject = GameObject.FindWithTag("Player");

                // Ensure player object exists
                Assert.IsNotNull(playerObject);

                // Create a new comparer for float
                var comparer = new FloatEqualityComparer(10e-6f);

                // Get the temp player controller from the component of player object
                TempPlayerController playerController = playerObject.GetComponent<TempPlayerController>();

                Debug.Log($"Power up: {playerController.currentPowerUp} (current) vs {loadedPlayerData.powerup} (save data)");
                // Ensure the player power up is loaded
                Assert.AreEqual(playerController.currentPowerUp, loadedPlayerData.powerup);

                // Get the current player position
                Vector3 currentPosition = playerObject.transform.position;

                Debug.Log($"Player Position x: {currentPosition.x} (current) vs {loadedPlayerData.position[0]} (save data)");
                // Ensure the x position is correct
                Assert.That(currentPosition.x, Is.EqualTo(loadedPlayerData.position[0]).Using(comparer));

                Debug.Log($"Player Position y: {currentPosition.y} (current) vs {loadedPlayerData.position[1]} (save data)");
                // Ensure the y position is correct
                Assert.That(currentPosition.y, Is.EqualTo(loadedPlayerData.position[1]).Using(comparer));

                // Get Game Manager from the scene
                GameManager gameManager = GameObject.FindObjectOfType<GameManager>();

                Debug.Log($"No. of enemies: {gameManager.GetEnemyDataList().Count} (current) vs {loadedPlayerData.enemiesData.Length} (save data)");
                // Ensure the no. of enemies are the same as the save data
                Assert.AreEqual(gameManager.GetEnemyDataList().Count, loadedPlayerData.enemiesData.Length);

                Debug.Log($"No. of boxes: {gameManager.GetBoxDataList().Count} (current) vs {loadedPlayerData.boxData.Length} (save data)");
                // Ensure the no. of boxes are the same as the save data
                Assert.AreEqual(gameManager.GetBoxDataList().Count, loadedPlayerData.boxData.Length);
            }
        }
    }
}
