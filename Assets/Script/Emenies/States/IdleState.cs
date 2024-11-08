using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    protected D_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOVer;

    protected bool isPlayerInMinAgroRange;

    protected float idleTime;
    public IdleState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_IdleState stateData) : base(enemyBase1, stateMachine, animatorBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        isPlayerInMinAgroRange = enemyBase1.CheckPlayerInMinAgroRange();
    }
    public override void Enter()
    {
        base.Enter();
        enemyBase1.SetVelocity(0f);
        isIdleTimeOVer = false;
        SetRandomIdleTime();
    }

        public override void Exit()
    {
        base.Exit();

        if ( flipAfterIdle)
        {
            enemyBase1.Flip();
        }
    }

        public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOVer = true;
        }

    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
