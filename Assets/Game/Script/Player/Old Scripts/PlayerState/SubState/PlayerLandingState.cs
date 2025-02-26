using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if(isAnimationFinish)
            {
                stateMachine.ChangeState(player.idleState);
            }

        }
    }
}
