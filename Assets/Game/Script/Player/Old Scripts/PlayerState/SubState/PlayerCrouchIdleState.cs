using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        player.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isExitingState)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.crouchMoveState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }
}
