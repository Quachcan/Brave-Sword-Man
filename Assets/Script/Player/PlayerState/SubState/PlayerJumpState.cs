using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilitiesState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput(); 
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilitiesDone = true;
        amountOfJumpsLeft--;
        player.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void ResetAmountOfJumpLefts() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DeceaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
