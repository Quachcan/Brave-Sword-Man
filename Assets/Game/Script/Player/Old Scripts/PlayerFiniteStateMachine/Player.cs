using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandingState landingState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMoveState crouchMoveState { get; private set; } 
    //public PlayerAttackState primaryAttackState { get; private set; }
    //public PlayerAttackState secondaryAttackState { get; private set; } 

    public Animator animator{ get; private set; }
    public PlayerInputHandler inputHandler{ get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Transform dashDirectionIndicator { get; private set; }
    public BoxCollider2D movementCollider { get; private set; }
    public PlayerInventory inventory { get; private set; }

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform ceilingCheck;
    

    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    private Vector2 workSpace;

    public void Awake()
    {
        StateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        jumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        inAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        landingState = new PlayerLandingState(this, StateMachine, playerData, "land");
        wallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        wallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        wallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        wallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        ledgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData,"ledgeClimbState");
        dashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        crouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        crouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        //primaryAttackState = new PlayerAttackState(this, StateMachine, playerData,"attack");
        //secondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }
    
    public void Start()
    {
        animator = GetComponent<Animator>(); 
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        dashDirectionIndicator = transform.Find("DashDirectionIndicator");
        movementCollider = GetComponent<BoxCollider2D>();
        inventory = GetComponent<PlayerInventory>();

        facingDirection = 1;

        //primaryAttackState.SetWeapon(inventory.weapons[(int)CombatInput.primaryInput]);

        StateMachine.Initialize(idleState);
    }

    private void Update()
    {
        currentVelocity = rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicUpdate();      
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        rb.velocity = workSpace;
        currentVelocity = workSpace;
    }
    public void SetVelocityY(float velocity)
    {
        workSpace.Set(currentVelocity.x, velocity);
        rb.velocity = workSpace;
        currentVelocity = workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity * direction);
        rb.velocity = workSpace;
        currentVelocity = workSpace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workSpace = direction * velocity;
        rb.velocity = workSpace;
        currentVelocity = workSpace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

        public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = movementCollider.offset;
        workSpace.Set(movementCollider.size.x, height);
        
        center.y += (height - movementCollider.size.y) / 2;

        movementCollider.size = workSpace;
        movementCollider.offset = center;
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workSpace.Set((xDist + 0.015f) * facingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDist = yHit.distance;

        workSpace.Set(wallCheck.position.x + (xDist * facingDirection), ledgeCheck.position.y - yDist);
        return workSpace;
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void Die()
    {
        animator.SetTrigger("die");
        GameManager.Instance.OnPlayerDeath();
        Destroy(gameObject);
    }

}
