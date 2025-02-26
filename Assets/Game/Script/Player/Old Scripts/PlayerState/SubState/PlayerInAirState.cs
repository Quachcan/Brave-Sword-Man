using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //-----------INPUT-------------------
    private int xInput;
    private bool JumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    //-----CHECKS------------------------
    private bool isGrounded;
    private bool isTouchingWall;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private float startWallJumpCoyoteTime;


    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckJumpCoyoteTime();

        xInput = player.inputHandler.normInputX;
        JumpInput = player.inputHandler.jumpInput;
        jumpInputStop = player.inputHandler.jumpInputStop;
        grabInput = player.inputHandler.grabInput;
        dashInput = player.inputHandler.dashInput;

        CheckJumpMultiplier();

        if(player.inputHandler.attackInput[(int)CombatInput.primaryInput])
        {
            //stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.attackInput[(int) CombatInput.secondaryInput])
        {
            //stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if(isGrounded && player.currentVelocity.y <0.01f)
        {
            stateMachine.ChangeState(player.landingState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
        else if(JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime)) 
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CheckIfTouchingWall();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if(JumpInput && player.jumpState.CanJump())
        {
            coyoteTime = false;
            stateMachine.ChangeState(player.jumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (isTouchingWall && xInput == player.facingDirection && player.currentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if(dashInput && player.dashState.CheckCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity *xInput);

            player.animator.SetFloat("yVelocity", player.currentVelocity.y);
            player.animator.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0f) 
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void DoCheck()
    {
        base.DoCheck();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingLedge = player.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.ledgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.jumpState.DeceaseAmountOfJumpsLeft();
        }
    }

    private void CheckJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime() 
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }
    public void StopWallJumpCoyoteTime() =>wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
}
