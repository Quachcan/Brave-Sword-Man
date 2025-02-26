using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerWallSlideState : OldPlayerTouchingWallState
{
    public OldPlayerWallSlideState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isExitingState)
        {
        player.SetVelocityY(playerData.wallSlideVelocity);

        if (grabInput && yInput ==0)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }

        }
    }
}
