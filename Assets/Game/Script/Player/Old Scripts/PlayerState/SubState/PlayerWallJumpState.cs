using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilitiesState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput();
        player.jumpState.ResetAmountOfJumpLefts();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
        player.jumpState.DeceaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.animator.SetFloat("yVelocity", player.currentVelocity.y);
        player.animator.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));

        if(Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilitiesDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = player.facingDirection;
        }
        else
        {
            wallJumpDirection = -player.facingDirection;
        }
    }
}
