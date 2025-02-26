using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerWallJumpState : OldPlayerAbilitiesState
{
    private int wallJumpDirection;
    public OldPlayerWallJumpState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
