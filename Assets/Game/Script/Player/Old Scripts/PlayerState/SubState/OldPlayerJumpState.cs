using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerJumpState : OldPlayerAbilitiesState
{
    private int amountOfJumpsLeft;
    public OldPlayerJumpState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
