using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    protected Enemy1 enemy;
    public E1_ChargeState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_ChargeState stateData, Enemy1 enemy) : base(enemyBase1, stateMachine, animatorBoolName, stateData)
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


        if (performCloseRangeAction)
        {
            stateMachine.ChangeState (enemy.meleeAttackState);
        }

        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }

        else if (isChargeTimeOver)
        {
            
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }

            else 
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
        
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
