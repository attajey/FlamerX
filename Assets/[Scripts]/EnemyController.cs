/*  Filename:           EnemyController.cs
 *  Author(s):          Atta Jirofty
 *  Last Update:        Jan 25, 2023
 *  Description:        Enemy controller for taking damage and AI. 
 *  Revision History:   Jan 25, 2023 (Atta Jirofty): Initial script.
 *                      Jan 25, 2023 (Atta Jirofty): Added take damage.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float speed;
    [SerializeField] private float rangeToChase = 5f;
    [SerializeField] private float rangeToAttack = 5f;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject player;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRate = 1f;
    private float nextAttackTime = 0f;

    private float distanceToPlayer;
    private int currentHealth;

    private int isAttacking = Animator.StringToHash("isAttacking");


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();
        if (Time.time >= nextAttackTime)
        {
            if (distanceToPlayer <= rangeToAttack)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }


        //if (distanceToPlayer <= rangeToChase)
        //{
        //    if (player.transform.localScale.x == transform.localScale.x)
        //    {
        //        Vector3 temp = transform.localScale;
        //        temp.x *= -1;
        //        transform.localScale = temp;

        //    }
        //    //Debug.Log("chasing");
        //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //}
        //else
        //{
        //    this.GetComponent<MovingObject>().Move();
        //}
    }

    private void Attack()
    {
        anim.SetTrigger(isAttacking);
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<CharacterController>().TakeDamage(attackDamage);

        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt anim
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play die anim
        anim.SetBool("isDead", true);

        // Disable the enemy
        Destroy(this.gameObject, 2f);
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
