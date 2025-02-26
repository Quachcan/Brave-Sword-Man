using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerWallClimbState : OldPlayerTouchingWallState
{

    public OldPlayerWallClimbState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
        player.SetVelocityY(playerData.wallClimbVelocity);

        if (yInput != 1)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }

        }
    }
}
