using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerLandingState : OldPlayerGroundedState
{
    public OldPlayerLandingState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
