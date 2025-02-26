using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 enemy;
    public E1_LookForPlayerState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayer stateData, Enemy1 enemy) : base(enemyBase1, stateMachine, animatorBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
