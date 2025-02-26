using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerGroundedState : OldPlayerState
{

protected int xInput;
protected int yInput;

protected bool isTouchingCeiling;

private bool jumpInput;
private bool grabInput;
private bool dashInput;
private bool isGrounded;
private bool isTouchingWall;
private bool isTouchingLedge;

    public OldPlayerGroundedState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
        isTouchingCeiling = player.CheckForCeiling();
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetAmountOfJumpLefts();
        player.dashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.inputHandler.normInputX;
        yInput = player.inputHandler.normInputY;
        jumpInput = player.inputHandler.jumpInput;
        grabInput = player.inputHandler.grabInput; 
        dashInput = player.inputHandler.dashInput;

        if(player.inputHandler.attackInput[(int)CombatInput.primaryInput] && !isTouchingCeiling)
        {
            //stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.attackInput[(int) CombatInput.secondaryInput] && !isTouchingCeiling)
        {
            //stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if(jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if(dashInput && player.dashState.CheckCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
