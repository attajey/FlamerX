/*  Filename:           WindowedToggle.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        A toggle to switch between a windowed or a full screen
 *  Revision History:   November 25, 2022 (Yuk Yee Wong): Initial script.
 *                      December 12, 2022 (Yuk Yee Wong): Fix bug for builds.
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class <c>WindowedToggle</c> is a toggle to switch between a windowed or a full screen
/// </summary>
public class WindowedToggle : Toggle
{
    private FullScreenToggle fullScreenToggle;

    protected override void OnEnable()
    {
        base.OnEnable();

        // Find the full screen toggle in the scene
        fullScreenToggle = FindObjectOfType<FullScreenToggle>();

        // Change the toggle to on if current mode is windowed
        isOn = IsCurrentModeWindowed();

        // Add on value change listener
        onValueChanged.AddListener(OnWindowedToogleValueChanged);

        // add full screen toggle change listener
        AddFullScreenToggleValueChangeListener();
    }

    private bool IsCurrentModeWindowed()
    {
        Debug.Log($"IsCurrentModeWindowed {Screen.fullScreenMode}");
        return Screen.fullScreenMode == FullScreenMode.Windowed;
    }

    private void OnWindowedToogleValueChanged(bool value)
    {
        // Update the full screen mode
        if (fullScreenToggle != null)
        {            
            Screen.SetResolution(fullScreenToggle.CurrentResolutionWidth, fullScreenToggle.CurrentResolutionHeight, !value);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !value);
        }
    }

    private void AddFullScreenToggleValueChangeListener()
    {
        // Assign the value change listener to the full screen toggle if it exists
        if (fullScreenToggle != null)
        {
            fullScreenToggle.onValueChanged.AddListener(OnFullScreenToggleValueChange);
        }
    }

    private void OnFullScreenToggleValueChange(bool value)
    {
        // if it is not full screen, the screen should be windowed
        if (!value)
        {
            isOn = true;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        // Remove on value change listener
        onValueChanged.RemoveListener(OnWindowedToogleValueChanged);

        // Remove full screen toggle change listener
        RemoveFullScreenToggleValueChangeListener();
    }

    private void RemoveFullScreenToggleValueChangeListener()
    {
        // Find the full screen toggle in the scene
        FullScreenToggle fullScreenToggle = FindObjectOfType<FullScreenToggle>();

        // Remove the value change listener from the full screen toggle if it exists
        if (fullScreenToggle != null)
        {
            fullScreenToggle.onValueChanged.RemoveListener(OnFullScreenToggleValueChange);
        }
    }
}
