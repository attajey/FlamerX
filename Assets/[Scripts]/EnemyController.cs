/*  Filename:           EnemyController.cs
 *  Author(s):          Atta Jirofty
 *  Last Update:        Jan 25, 2023
 *  Description:        Enemy controller for taking damage and AI. 
 *  Revision History:   Jan 25, 2023 (Atta Jirofty): Initial script.
 *                      Jan 25, 2023 (Atta Jirofty): Added take damage.
 *                      Feb 08, 2023 (Atta Jirofty): Added SFX
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float speed;
    //[SerializeField] private float rangeToChase = 5f;
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

    [Header("SFX")]
    [SerializeField] private AudioClip[] attackSFX;
    private AudioSource audioSource;

    private float distanceToPlayer;
    private int currentHealth;

    private int isAttacking = Animator.StringToHash("isAttacking");
    private int isDead = Animator.StringToHash("isDead");

    private bool isNotDeadYet = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();
        if (currentHealth > 0)
        {
            if (Time.time >= nextAttackTime)
            {
                if (distanceToPlayer <= rangeToAttack)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                //if (currentHealth <= 0)
                //{


                //}
            }
        }
        else if (currentHealth <= 0)
        {
            Die();

            //if (isNotDeadYet)
            //{
            //    ScoreScript.scoreValue += 100;
            //    Die();
            //    isNotDeadYet = false;

            //}
            isNotDeadYet = false;

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
        PlayAttackSFX();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }


    void PlayAttackSFX()
    {
        switch (this.gameObject.name)
        {
            case "Skeleton Model":
                audioSource.clip = attackSFX[0];
                break;
            case "Mushroom Model":
                audioSource.clip = attackSFX[1];
                break;
            case "Goblin Model":
                audioSource.clip = attackSFX[2];
                break;
            case "Flying eye Model":
                audioSource.clip = attackSFX[3];
                break;
            default:
                Debug.Log("SFX for enemy attack not found");
                break;
        }
        audioSource.Play();
        //    CallAudio();
    }

    void Die()
    {
        if (isNotDeadYet) { 

            // Play die anim
            anim.SetTrigger(isDead);
            ScoreScript.scoreValue += 100;

            // Disable the enemy
            Destroy(this.gameObject, 2f);
        }
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
