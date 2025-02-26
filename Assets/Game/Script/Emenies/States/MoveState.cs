using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{

    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    protected bool isPlayerInMinAgroRange;
    

    public MoveState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_MoveState stateData) : base(enemyBase1, stateMachine, animatorBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = enemyBase1.CheckWall();
        isDetectingLedge = enemyBase1.CheckLedge();
        isPlayerInMinAgroRange = enemyBase1.CheckPlayerInMinAgroRange();
    }
    public override void Enter()
    {
        base.Enter();
        enemyBase1.SetVelocity(stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

    }

}
