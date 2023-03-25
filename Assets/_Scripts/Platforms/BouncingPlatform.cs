/*  Filename:           BouncingPlatform.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 5, 2022
 *  Description:        A bounce platform that makes the player bounce when collides 
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

/// <summary>
/// class <c>BouncingPlatform</c> makes the player bounce when collides 
/// </summary>
public class BouncingPlatform : MonoBehaviour
{
    [SerializeField] private float multiplier;

    [Header("SFX")]
    [SerializeField] private AudioSource bouncingSFX;

    /// <summary>
    /// Initiate player bouncing when collides
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        // if player collides, then player bounces and bounce sfx is played
        if (IsPlayer(other.gameObject))
        {
            TempPlayerController player = other.gameObject.GetComponent<TempPlayerController>();
            player.Bounce();
            PlayBounceSFX();
        }
        // if no, stop playing the bouncing sfx
        else
        {
            StopBounceSFX();
        }
    }

    private bool IsPlayer(GameObject other)
    {
        return other.CompareTag("Player");
    }

    private void StopBounceSFX()
    {
        bouncingSFX.Stop();
    }

    public void PlayBounceSFX()
    {
        bouncingSFX.Play();
    }
}
