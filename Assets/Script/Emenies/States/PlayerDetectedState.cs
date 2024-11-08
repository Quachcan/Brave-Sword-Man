using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    
    public PlayerDetectedState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_PlayerDetected stateData) : base(enemyBase1, stateMachine, animatorBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        isPlayerInMinAgroRange = enemyBase1.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyBase1.CheckPlayerInMaxAgroRange();

        performCloseRangeAction = enemyBase1.CheckPlayerInCloseRangeAction();
    }
    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        enemyBase1.SetVelocity(0f);

    }
    
        public override void Exit()
    {
        base.Exit();
    }

        public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
