using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{

    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    public ChargeState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_ChargeState stateData) : base(enemyBase1, stateMachine, animatorBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        isPlayerInMinAgroRange = enemyBase1.CheckPlayerInMinAgroRange();
        isDetectingLedge = enemyBase1.CheckLedge();
        isDetectingWall = enemyBase1.CheckWall();

        performCloseRangeAction = enemyBase1.CheckPlayerInCloseRangeAction();
    }
    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;
        enemyBase1.SetVelocity(stateData.chargeSpeed);
    }

        public override void Exit()
    {
        base.Exit();
    }

        public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
