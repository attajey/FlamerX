/*  Filename:           ChangePowerUpTrigger.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 20, 2022
 *  Description:        A trigger to update the player's current power up
 *  Revision History:   November 20, 2022 (Yuk Yee Wong): Initial script.
 */

using UnityEngine;

/// <summary>
/// class <c>ChangePowerUpTrigger</c> is used to update the player's current power up by a trigger
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ChangePowerUpTrigger : MonoBehaviour
{
    [SerializeField] private TempPlayerController.Powerup powerUp;
    [SerializeField] private GameObject destroyEffect;

    /// <summary>
    /// Update player's curernt power up when the player triggers
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player triggers, then update the player's current power up, instantiate the destroy effect, and destroy the game object
        if (IsPlayer(other.gameObject))
        {
            TempPlayerController playerController = other.GetComponent<TempPlayerController>();
            if (playerController != null)
            {
                // Update the player's current power up
                playerController.UpdateCurrentPowerUp(powerUp);

                // Instantiate the destroy effect in the current transform position
                GameObject effect = Instantiate(destroyEffect);
                effect.transform.position = transform.position;

                // Destroy the game object
                Destroy(gameObject);
            }
        }
    }

    private bool IsPlayer(GameObject other)
    {
        return other.CompareTag("Player");
    }

}
