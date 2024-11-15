using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
