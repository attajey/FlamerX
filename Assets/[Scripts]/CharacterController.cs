/*  Filename:           CharacterController.cs
 *  Author(s):             Atta Jirofty
 *  Last Update:        Jan 25, 2023
 *  Description:        Player controller to move, attack, pause, and resume
 *  Revision History:   Jan 24, 2023 (Atta Jirofty): Initial script.
 *                      Jan 25, 2023 (Atta Jirofty): Added player movement.
 *                      Jan 25, 2023 (Atta Jirofty): Added player attack system.
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float runningMultiplier = 1.4f;
    [SerializeField] private float jumpForce = 20f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 40;
    [SerializeField] private float attackRate = 2f;
    private float nextAttackTime = 0f;

    [Header("SFX")]
    [SerializeField] private AudioClip[] jumpSFX;
    [SerializeField] private AudioClip attackSFX;
    private AudioSource audioSource;

    private Animator anim;
    private Rigidbody2D rBody;

    private bool isFacingRight = true;
    private int isAttacking = Animator.StringToHash("isAttacking");
    private int isJumping = Animator.StringToHash("isJumping");

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    //TODO: Delete all hard coded strings and make them variables

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
        currentHealth = maxHealth;



    }

    private void Update()
    {
        // attacking
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        float horiz = Input.GetAxis("Horizontal");
        rBody.velocity = new Vector2(
            IsPressingRunButton() ?
            horiz * walkingSpeed * runningMultiplier : horiz * walkingSpeed, rBody.velocity.y);

        // Vertical movement
        if (IsPressingJumpButton() && IsGrounded())
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            PlayRandomJumpSFX();
            anim.SetTrigger(isJumping);
        }

        // Flip the character for direction change
        if ((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }

        anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("yVelocity", rBody.velocity.y);
        anim.SetBool("isGrounded", IsGrounded());
    }

    private void Attack()
    {
        // Play attack anim
        anim.SetTrigger(isAttacking);
        PlayAttackSound();

        // Detect enemies or boxes in range of attack
        Collider2D[] hitEnemiesOrBoxes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies in range of attack
        foreach (Collider2D enemyOrBox in hitEnemiesOrBoxes)
        {
            if (enemyOrBox.CompareTag("Enemy")) // To eliminate Confiner from hitEnemies array. Confiner is for Cinemachine ? 
            {
                enemyOrBox.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
            else if (enemyOrBox.CompareTag("Box"))
            {
                enemyOrBox.GetComponent<Box>().DestroySelf();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt anim
        //anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Debug.Log("Character is dead!");
            //Die();
        }
    }
    //void Die()
    //{
    //    // Play die anim
    //    anim.SetBool("isDead", true);

    //    // Disable the enemy
    //    Destroy(this.gameObject, 2f);
    //}

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private bool IsPressingJumpButton()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    private bool IsPressingRunButton()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }

    void PlayRandomJumpSFX()
    {
        audioSource.clip = jumpSFX[UnityEngine.Random.Range(0, jumpSFX.Length)];
        audioSource.Play();
        //    CallAudio();
    }

    void PlayAttackSound()
    {
        audioSource.clip = attackSFX;
        audioSource.Play();
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
