using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{

    private Enemy1 enemy;
    public E1_MoveState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData, Enemy1 enemy) : base(enemyBase1, stateMachine, animatorBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }

        else if( isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
