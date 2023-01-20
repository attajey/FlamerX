/*  Filename:           TempPlayerController.cs
 *  Author:             Yuk Yee Wong, Atta Jirofty, Mohammadfarid Fakori, Juan Munoz Rivera, Kevin Curiel Justo
 *  Last Update:        December 7, 2022
 *  Description:        Player controller to move, attack, pause, and resume
 *  Revision History:   November 5, 2022 (Yuk Yee Wong): Initial script.
 *                      November 16, 2022 (Atta Jirofty): Added audio.
 *                      November 19, 2022 (Mohammadfarid Fakori): Added pause and resume function.
 *                      November 20, 2022 (Juan Munoz Rivera): Added update current power up.
 *                      November 20, 2022 (Yuk Yee Wong): Used enemy's damagable method to 
 *                      November 26, 2022 (Juan Munoz Rivera): Added debug log.
 *                      November 29, 2022 (Mohammadfarid Fakori): Enable and disable settings canvas upon pause and resume. 
 *                      December 2, 2022 (Kevin Curiel Justo): Integrated the input system.
 *                      December 7, 2022 (Yuk Yee Wong): Reorganised the code and loaded the rebind input on awake.
 */

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player controller to move, attack, pause, and resume
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class TempPlayerController : MonoBehaviour, IBounceable
{
    [Header("Movement")]
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float jumpForce = 800.0f;
    [SerializeField] private float runningMultiplier = 1.5f;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos1;
    [SerializeField] private Transform groundCheckPos2;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Bouncing Props")]
    [SerializeField] private float involuntaryBounceMultiplier = 1.3f;
    [SerializeField] private float voluntaryBounceMultiplier = 1.6f;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;

    [Header("Projectile Props")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileStartingPoint;
    [SerializeField] private float projectileVelocity = 5f;

    [Header("SFX")]
    [SerializeField] private AudioSource jumpingSFX;
    [SerializeField] private AudioSource attackingSFX;

    [Header("Debug")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isBouncing = false;

    private float yVelocityOffset = 0.01f;
    private bool isFacingRight = true;

    private Rigidbody2D rBody;
    private Animator anim;

    public PlayerInput playerInput = null; // access by KeybindingMenu.cs

    private int AttackButtonPressed = Animator.StringToHash("isAttacking");

    public enum Powerup
    {
        Sword,
        Blaster
    }

    public Powerup currentPowerUp;

    private void Awake()
    {
        // Get rigidbody from component
        rBody = GetComponent<Rigidbody2D>();

        // Get animator from component
        anim = GetComponent<Animator>();

        // Rebind input actions
        RebindInputActions();
    }

    private void RebindInputActions()
    {
        // Get rebind data from key binding menu
        string rebinds = KeybindingMenu.GetRebindData();

        // Rebind if the string is not null or empty
        if (!string.IsNullOrEmpty(rebinds))
        {
            playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        }
    }

    private void FixedUpdate()
    {
        // Vertical Movement ( JUMP )
        UpdateIsGrounded(GroundCheck() && Mathf.Abs(rBody.velocity.y) < yVelocityOffset);
    }

    private void UpdateIsGrounded(bool grounded)
    {
        if (!isGrounded)
        {
            transform.eulerAngles = Vector3.zero;
        }

        if (isGrounded != grounded)
        {
            isGrounded = grounded;
        }
    }

    /// <summary>
    /// Use Physics2D.OverlapCircle to do the ground check
    /// </summary>
    /// <returns></returns>
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos1.position, groundCheckRadius, whatIsGround)
            || Physics2D.OverlapCircle(groundCheckPos2.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    #region Player Input Actions
    /// <summary>
    /// Play different attack actions depends on the current power up
    /// </summary>
    /// <param name="context"></param>
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (currentPowerUp)
            {
                case Powerup.Sword:
                    anim.SetTrigger(AttackButtonPressed);
                    SwordAttack();
                    PlayAttackSFX();
                    break;
                case Powerup.Blaster:
                    anim.SetTrigger(AttackButtonPressed);
                    BlasterAttack();
                    PlayAttackSFX();
                    break;
            }
        }

        if (context.performed)
            anim.ResetTrigger(AttackButtonPressed);
    }

    private void SwordAttack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Check every collider to see if there is any enemy
        foreach (Collider2D collider in hitColliders)
        {
            // Damage the enemy if the enemy is inside the attack range
            if (IsEnemy(collider.gameObject))
            {
                Enemy enemyComponent = collider.GetComponentInChildren<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.Damage();
                }
            }
        }
    }

    private bool IsEnemy(GameObject other)
    {
        return other.CompareTag("Enemy");
    }

    private void BlasterAttack()
    {
        // Instantiate projectile
        Projectile projectile = Instantiate(projectilePrefab);

        // Reposition the projectile
        projectile.transform.position = projectileStartingPoint.transform.position;

        // Set projectile velocity according to the facing direction
        projectile.SetVelocity(isFacingRight ? projectileVelocity : -projectileVelocity);
    }

    private void PlayAttackSFX()
    {
        attackingSFX.Play();
    }

    /// <summary>
    /// Move the player according to the input
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        // Horizontal Movement
        float horiz = context.ReadValue<Vector2>().x;
        rBody.velocity = new Vector2(
            IsPressingRunButton() ? horiz * speed * runningMultiplier : horiz * speed,
            rBody.velocity.y);

        // Flip Player Sprite
        if ((isFacingRight && rBody.velocity.x < 0)
            || (!isFacingRight && rBody.velocity.x > 0))
        {
            Flip();
        }

        anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("yVelocity", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    /// <summary>
    /// Use default key to determine whether the player pressed running button(s)
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Player jumps when jump button is pressed
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {        
        if (isGrounded && (context.performed || isBouncing))
        {
            // Add force to the rigidbody
            // If it is bouncing, then multiply the jump force by the multiplier
            if (isBouncing)
            {
                rBody.AddForce(new Vector2(0.0f, context.performed ?
                    jumpForce * voluntaryBounceMultiplier
                    : jumpForce * involuntaryBounceMultiplier));
                isBouncing = false;
            }
            else
            {
                rBody.AddForce(new Vector2(0.0f, jumpForce));
            }

            UpdateIsGrounded(false);
            PlayJumpingSFX();
        }        
    }

    private void PlayJumpingSFX()
    {
        jumpingSFX.Play();
    }
    #endregion Player Input Actions

    /// <summary>
    /// Change power up
    /// </summary>
    /// <example>Called by the ChangePowerUpTrigger.cs</example>
    /// <param name="powerup"></param>
    public void UpdateCurrentPowerUp(Powerup powerup)
    {
        currentPowerUp = powerup;

        anim.SetBool("SwordPowerUp", false);

        anim.SetBool("BlasterPowerUp", false);

        switch (powerup)
        {
            case Powerup.Sword:
                anim.SetBool("SwordPowerUp", true);
                break;
            case Powerup.Blaster:
                anim.SetBool("BlasterPowerUp", true);
                break;
        }
    }

    /// <summary>
    /// Update the isBouncing boolean
    /// <example>Called by the BouncingPlatform.cs</example>
    /// </summary>
    public void Bounce()
    {
        isBouncing = true;
    }
}
