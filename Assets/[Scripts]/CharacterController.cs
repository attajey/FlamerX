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
    private bool isGrounded;

    [Header("Auto get component")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rBody;

    private bool isFacingRight = true;
    private int isAttacking = Animator.StringToHash("isAttacking");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // attacking
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger(isAttacking);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // horizontal movement
        float horiz = Input.GetAxis("Horizontal");
        rBody.velocity = new Vector2(
            isPressingRunButton() ? horiz * walkingSpeed * runningMultiplier : horiz * walkingSpeed, rBody.velocity.y);

        // vertical movement
        if (isPressingJumpButton() && isGrounded)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("isJumping");
        }

        // flip the character for direction change
        if ((isFacingRight && rBody.velocity.x < 0) || (!isFacingRight && rBody.velocity.x > 0)) 
        { 
            Flip();
        }

        anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("yVelocity", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }


    private bool isPressingJumpButton()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
    }

    private bool isPressingRunButton()
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
}
