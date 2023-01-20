/*  Filename:           VolumeSlider.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 30, 2022
 *  Description:        Contains functions to change volume of master, sfx, and music
 *  Revision History:   November 30, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// class <c>VolumeSlider</c> contains functions to change volume of master, sfx, and music
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType
    {
        NONE = 0,
        MASTER = 1,
        MUSIC = 2,
        SFX = 3,
    }

    [SerializeField] private VolumeType type;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeParameter;
    private Slider slider;
    private static float MasterVolume = 0;
    private static float MusicVolume = 0;
    private static float SfxVolume = 0;
    private const float lowestVolume = -80f;
    private const float highestVolume = 20f;

    private void Awake()
    {
        // Get the slider from children
        slider = GetComponentInChildren<Slider>();

        // Update slider value
        switch (type)
        {
            case VolumeType.MASTER:
                slider.value = (MasterVolume - lowestVolume) / (highestVolume - lowestVolume);
                break;
            case VolumeType.MUSIC:
                slider.value = (MusicVolume - lowestVolume) / (highestVolume - lowestVolume);
                break;
            case VolumeType.SFX:
                slider.value = (SfxVolume - lowestVolume) / (highestVolume - lowestVolume);
                break;
            default:
                Debug.LogError($"{type} is not specified in VolumeSlider, please update the type in Awake()");
                break;
        }

        // Add listner to volume slider
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    /// <summary>
    /// Change volume of the designated volume type by updating the float in audio mixer
    /// </summary>
    /// <param name="newVolume">newVolume should be between 0 to 1</param>
    private void ChangeVolume(float newVolume)
    {
        // Update volume proportionally
        float volume = newVolume * (highestVolume - lowestVolume) + lowestVolume;

        // Set audio mixer volume
        audioMixer.SetFloat(volumeParameter, volume);

        switch (type)
        {
            case VolumeType.MASTER:
                MasterVolume = volume;
                break;
            case VolumeType.MUSIC:
                MusicVolume = volume;
                break;
            case VolumeType.SFX:
                SfxVolume = volume;
                break;
            default:
                Debug.LogError($"{type} is not specified in VolumeSlider, please update the type in Awake()");
                break;
        }        
    }
}
