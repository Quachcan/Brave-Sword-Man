using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }
        public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        player.SetVelocityX(playerData.movementVelocity * xInput);
        
        if(!isExitingState)
        {
            if(xInput == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else if(yInput == -1)
            {
                stateMachine.ChangeState(player.crouchMoveState);
            }

        }
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }
}
