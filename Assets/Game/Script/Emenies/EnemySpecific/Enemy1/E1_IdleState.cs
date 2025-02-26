using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{   
    private Enemy1 enemy; 
    public E1_IdleState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData, Enemy1 enemy) : base(enemyBase1, stateMachine, animatorBoolName, stateData)
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

        else if (isIdleTimeOVer )
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
