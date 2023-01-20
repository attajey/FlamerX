/*  Filename:           Enemy.cs
 *  Author:             Yuk Yee Wong (301234795)
 *  Last Update:        November 20, 2022
 *  Description:        Enemy behaviour to take damage and die upon player attack
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 *                      November 20, 2022 (Yuk Yee Wong): Implement IDamagable.
 */

using UnityEngine;

/// <summary>
/// class <c>Enemy</c> takes damage and die upon player attack
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
            {
                IDamageable damageable = projectile;
                if (damageable != null)
                {
                    damageable.Damage();
                }
            }

            Damage();
        }
    }

    public void Damage()
    {
        GameObject effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(enemyObject);
    }
}
