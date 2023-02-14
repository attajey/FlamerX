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
    [SerializeField] private GameObject bullet;
    private float nextAttackTime = 0f;

    [Header("SFX")]
    [SerializeField] private AudioClip[] attackSFX;
    private AudioSource audioSource;

    private float distanceToPlayer;
    private int currentHealth;

    private int isAttacking = Animator.StringToHash("isAttacking");
    private int isDead = Animator.StringToHash("isDead");

    private bool isNotDeadYet = true;
    private bool isFacingRight;

    private Rigidbody2D playerRigidBody;
    private Rigidbody2D rbody;

    public static event Action OnEnemyKilled;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        rbody = this.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    void Update()
    {
        RaycastHit2D playerDetected = Physics2D.Raycast(transform.position, -Vector2.up);
        if (playerDetected.collider.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (currentHealth > 0)
        {
            if (Time.time >= nextAttackTime)
            {
                if (distanceToPlayer <= rangeToAttack)
                {
                    FaceTo(player.transform);
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
        else if (currentHealth <= 0)
        {
            Die();
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

    private void FaceTo(Transform target)
    {
        Vector3 targ = target.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = 0f;//targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
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
        if (gameObject.name.Equals("Flying eye Model"))
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
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
        if (isNotDeadYet)
        {

            // Play die anim
            anim.SetTrigger(isDead);
            //ScoreText.scoreValue += 100;
            OnEnemyKilled?.Invoke();

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
