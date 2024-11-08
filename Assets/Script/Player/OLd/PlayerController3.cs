using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{

    private float movementInputDirection;
    private float dashTimer; 
    private float knockBackStartTime;
    [SerializeField]
    private float knockBackDuration;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;
    private bool canFlip;
    private bool isDashing;
    private bool knockBack;

    [SerializeField]
    private Vector2 knockbackSpeed;

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float maxStamina = 100f;
    public float stamina;
    public float dashStaminaCost = 20f;
    public float staminaRegenRate = 10f;


    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    public Vector2 dodgeDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    //public Transform legdeCheck;

    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        stamina = maxStamina;
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        //CheckLedgeClimb();
        RegenerateStamina();
        CheckDash();
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    public bool GetDashStatus()
    {
        return isDashing;
    }
    public void KnockBack(int direction)
    {
        knockBack = true;
        knockBackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if ( Time.time >= knockBackStartTime + knockBackDuration && knockBack)
        {
            knockBack = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    private void CheckDash()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                EndDash();
            }
        }
    }
    private void EndDash()
    {
        isDashing = false;
        rb.velocity = new Vector2(0, rb.velocity.y);  // Dừng lại sau khi dash
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

    }

    private void CheckIfCanJump()
    {
        if ((isGrounded && rb.velocity.y <= 0) || isWallSliding)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }

    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        isWalking = movementInputDirection != 0;
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Dash") && !isDashing)
        {
            Dash();  // Kích hoạt dash khi người chơi nhấn phím Dash
        }

    }

    private void RegenerateStamina()
    {
        if (!isDashing && stamina < maxStamina)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);  // Giới hạn stamina trong khoảng [0, maxStamina]
        }
    }

    private void Jump()
    {
        if (canJump && !isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        else if (isWallSliding && movementInputDirection == 0 && canJump) //Wall hop
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        else if ((isWallSliding || isTouchingWall) && movementInputDirection != 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    private void Dash()
    {
        if (isDashing || stamina < dashStaminaCost) return;  // Kiểm tra nếu đang dash hoặc không đủ stamina

        isDashing = true;
        dashTimer = dashDuration;
        stamina -= dashStaminaCost;  // Giảm stamina khi dash

        // Xác định hướng dash
        float dashDirection = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0);  // Thiết lập vận tốc dash

        anim.SetTrigger("Dash");  // Kích hoạt animation dash nếu có
    }


    private void ApplyMovement()
    {
        if (isDashing)
        {
            return; 
        }

        if (isGrounded && !knockBack)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection != 0 && knockBack)
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection == 0 && knockBack)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }



    private void Flip()
    {
        if (!isWallSliding && !knockBack)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            //transform.Rotate(0.0f, 180.0f, 0.0f);
            if (movementInputDirection > 0f)
        {
            transform.right = Vector3.right;
        }
        else if (movementInputDirection < 0f)
        {
            transform.right = Vector3.left;
        }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
