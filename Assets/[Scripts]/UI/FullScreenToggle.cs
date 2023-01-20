/*  Filename:           FullScreenToggle.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        December 12, 2022
 *  Description:        Toggle between a full screen or a smaller screen
 *  Revision History:   November 25, 2022 (Yuk Yee Wong): Initial script.
 *                      December 12, 2022 (Yuk Yee Wong): Fix bug for builds.
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class <c>FullScreenToggle</c> is a toggle to switch between a full screen or a smaller screen
/// </summary>
public class FullScreenToggle : Toggle
{
    private int resolutionWidth = 640;
    private int resolutionHeight = 480;

    // the screen update is not immediate, so windowed toggle listener will get wrong current resolution from screen
    // so storing these two parameters for reference
    public int CurrentResolutionWidth { get; private set; } 
    public int CurrentResolutionHeight { get; private set; }

    protected override void OnEnable()
    {
        base.OnEnable();

        CurrentResolutionWidth = Screen.width;
        CurrentResolutionHeight = Screen.height;

        // Change the toggle to on if current resolution is full screen
        isOn = IsCurrentResolutionFullScreen();

        // Add on value change listener
        onValueChanged.AddListener(OnFullScreenToogleValueChanged);

        // Add windowed toggle change listener
        AddWindowedToggleValueChangeListener();
    }

    private bool IsCurrentResolutionFullScreen()
    {
        Debug.Log($"IsCurrentResolutionFullScreen {Screen.currentResolution.width} {GetFullScreenResolution().width} {Screen.width}");
        Debug.Log($"IsCurrentResolutionFullScreen {Screen.currentResolution.height} {GetFullScreenResolution().height} {Screen.height}");

        return Screen.width.Equals(GetFullScreenResolution().width)
            && Screen.height.Equals(GetFullScreenResolution().height);
    }

    private Resolution GetFullScreenResolution()
    {
        return Screen.resolutions[Screen.resolutions.Length - 1];
    }

    private void OnFullScreenToogleValueChanged(bool value)
    {        
        // if the value is full screen, then set the resolution to full screen
        if (value)
        {
            CurrentResolutionWidth = GetFullScreenResolution().width;
            CurrentResolutionHeight = GetFullScreenResolution().height;
            Screen.SetResolution(CurrentResolutionWidth, CurrentResolutionHeight, Screen.fullScreenMode);
        }
        // if not, set a smaller screen size
        else
        {
            CurrentResolutionWidth = resolutionWidth;
            CurrentResolutionHeight = resolutionHeight;
            Screen.SetResolution(CurrentResolutionWidth, CurrentResolutionHeight, false);
        }
    }

    private void AddWindowedToggleValueChangeListener()
    {
        // Find the window toggle in the scene
        WindowedToggle windowedToggle = FindObjectOfType<WindowedToggle>();

        // Assign the value change listener to the windowed toggle if it exists
        if (windowedToggle != null)
        {
            windowedToggle.onValueChanged.AddListener(OnWindowedToggleValueChange);
        }
    }

    private void OnWindowedToggleValueChange(bool value)
    {
        // if it is not windowed, the screen size should be full screen
        if (!value)
        {
            isOn = true;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        // Remove on value change listener
        onValueChanged.RemoveListener(OnFullScreenToogleValueChanged);

        // Remove windowed toggle change listener
        RemoveWindowedToggleValueChangeListener();
    }

    private void RemoveWindowedToggleValueChangeListener()
    {
        // Find the window toggle in the scene
        WindowedToggle windowedToggle = FindObjectOfType<WindowedToggle>();

        // Remove the value change listener from the windowed toggle if it exists
        if (windowedToggle != null)
        {
            windowedToggle.onValueChanged.RemoveListener(OnWindowedToggleValueChange);
        }
    }
}
