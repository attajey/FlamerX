/*  Filename:           Projectile.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 20, 2022
 *  Description:        Projectile that damages itself when touches ground
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 *                      November 20, 2022 (Yuk Yee Wong): Implement IDamagable.
 */

using UnityEngine;

/// <summary>
/// Projectile that damages itself when touches ground
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IDamageable
{
    [SerializeField] private float xVelocity = 0;

    private Rigidbody2D rBody;

    private void Awake()
    {
        // Get rigidbody from component
        rBody = GetComponent<Rigidbody2D>();

        // Update rigidbody velocity
        UpdateRigidbodyVelocity();
    }

    private void UpdateRigidbodyVelocity()
    {
        rBody.velocity = new Vector2(xVelocity, 0);
    }

    /// <summary>
    /// Destroy itself when touches the ground
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy itself when touches the ground
        if (IsGround(other.gameObject))
        {
            Damage();
        }
    }

    private bool IsGround(GameObject other)
    {
        return other.CompareTag("Ground");
    }

    /// <summary>
    /// Damage function used by the IDamagable interface
    /// </summary>
    public void Damage()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Set the projectile velocity
    /// </summary>
    /// <param name="xVelocity"></param>
    public void SetVelocity(float xVelocity)
    {
        this.xVelocity = xVelocity;
        UpdateRigidbodyVelocity();
    }
}
