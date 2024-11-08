using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        
        holdPosition = player.transform.position;
        
        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();



        if(!isExitingState)
        {
            HoldPosition();
            if (yInput > 0)
            {
                stateMachine.ChangeState(player.wallClimbState);
            }
            else if(yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }

        }
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
