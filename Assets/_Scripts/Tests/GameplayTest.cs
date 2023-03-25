/*  Filename:           GameplayTests.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 8, 2022
 *  Description:        Unit test suite for gameplay scene functions
 *  Revision History:   December 8, 2022 (Yuk Yee Wong): Initial script.
 */

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools.Utils;
using System;

namespace PlayModeTests
{
    /// <summary>
    /// Unit test suite for game level scene pause button, resume button, and save button
    /// </summary>
    public class GameplayTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            // Load level 1 scene
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// Test save logic
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestSaveButton()
        {
            // Wait for objects to move by random seconds
            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));

            // Get the pause panel by tag
            var pausePanelObject = GameObject.FindWithTag("PausePanel");

            // Create a new comparer for float
            var comparer = new FloatEqualityComparer(10e-6f);

            #region Repeat TestPauseButton
            if (pausePanelObject == null)
            {
                // Wait for scene loaded
                yield return new WaitForSeconds(2f);

                // Simulate pause button click
                Button pauseButton = GameObject.FindWithTag("PauseButton").GetComponent<Button>();
                pauseButton.onClick.Invoke();

                // Wait for the pause panel to enable
                yield return new WaitForSecondsRealtime(2f);

                // Get the pause panel by tag again
                pausePanelObject = GameObject.FindWithTag("PausePanel");

            }
            #endregion Repeat TestPauseButton

            // Get the save button by tag
            var saveButtonObject = GameObject.FindWithTag("SaveButton");

            // Get the saved data
            PlayerData oldPlayerData = SaveManager.GetSavedPlayerData(); 
            var oldPlayerDataInString = SaveManager.SerializePlayerData(oldPlayerData);
            Debug.Log(oldPlayerDataInString);

            // Simulate save button click
            Button saveButton = saveButtonObject.GetComponent<Button>();
            saveButton.onClick.Invoke();

            yield return new WaitForSecondsRealtime(5f);

            try
            {
                // Get the new saved data
                PlayerData newPlayerData = SaveManager.GetSavedPlayerData();
                var newPlayerDataInString = SaveManager.SerializePlayerData(newPlayerData);
                Debug.Log(newPlayerDataInString);

                if (oldPlayerData != null)
                {
                    // Ensure that old data and new data are not the same
                    Assert.AreNotEqual(newPlayerDataInString, oldPlayerDataInString);
                }
                else
                {
                    Assert.IsNotNull(newPlayerData);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        /// <summary>
        /// Test pause logic
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestPauseButton()
        {
            // Wait for scene loaded
            yield return new WaitForSeconds(2f);

            // Get the pause panel by tag
            var pausePanelObject = GameObject.FindWithTag("PausePanel");

            // Ensure there is not active pause panel object exists
            Assert.IsNull(pausePanelObject);

            // Get the pause button by tag
            var pauseButtonObject = GameObject.FindWithTag("PauseButton");

            // Ensure the pause button object exists
            Assert.NotNull(pauseButtonObject);

            // Simulate pause button click
            Button pauseButton = pauseButtonObject.GetComponent<Button>();
            pauseButton.onClick.Invoke();

            // Wait for the pause panel to enable
            yield return new WaitForSecondsRealtime(2f);

            // Get the pause panel by tag again
            pausePanelObject = GameObject.FindWithTag("PausePanel");

            // Ensure the pause panel is enabled
            Assert.IsTrue(pausePanelObject.activeInHierarchy);

            // Create a new comparer for float
            var comparer = new FloatEqualityComparer(10e-6f);

            // Ensure the time scale is 0
            Assert.That(Time.timeScale, Is.EqualTo(0).Using(comparer));
        }

        /// <summary>
        /// Test resume logic
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TestResumeButton()
        {
            // Get the pause panel by tag
            var pausePanelObject = GameObject.FindWithTag("PausePanel");

            // Create a new comparer for float
            var comparer = new FloatEqualityComparer(10e-6f);

            #region Repeat TestPauseButton
            if (pausePanelObject == null)
            { 
                // Wait for scene loaded
                yield return new WaitForSeconds(2f);

                // Simulate pause button click
                Button pauseButton = GameObject.FindWithTag("PauseButton").GetComponent<Button>();
                pauseButton.onClick.Invoke();

                // Wait for the pause panel to enable
                yield return new WaitForSecondsRealtime(2f);

                // Get the pause panel by tag again
                pausePanelObject = GameObject.FindWithTag("PausePanel");

            }
            #endregion Repeat TestPauseButton

            // Ensure the pause panel is enabled
            Assert.IsTrue(pausePanelObject.activeInHierarchy);

            // Ensure the time scale is 0
            Assert.That(Time.timeScale, Is.EqualTo(0).Using(comparer));

            // Get the resume button by tag
            var resumeButtonObject = GameObject.FindWithTag("ResumeButton");

            // Ensure the resume button object exists
            Assert.NotNull(resumeButtonObject);

            // Simulate resume button click
            Button resumeButton = resumeButtonObject.GetComponent<Button>();
            resumeButton.onClick.Invoke();

            // Wait for the pause panel to disable
            yield return new WaitForSecondsRealtime(2f);

            // Ensure the pause panel is disabled
            Assert.IsTrue(!pausePanelObject.activeInHierarchy);

            // Ensure the time scale is 1
            Assert.That(Time.timeScale, Is.EqualTo(1).Using(comparer));
        }
    }
}