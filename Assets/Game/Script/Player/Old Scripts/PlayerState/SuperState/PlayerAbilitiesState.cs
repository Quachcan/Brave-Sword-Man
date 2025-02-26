using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesState : PlayerState
{

    protected bool isAbilitiesDone;

    private bool isGrounded;
    public PlayerAbilitiesState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilitiesDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAbilitiesDone)
        {
            if(isGrounded && player.currentVelocity.y < 0.0f)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else 
            {
                stateMachine.ChangeState(player.inAirState);
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

        isGrounded= player.CheckIfGrounded();
    }
}
