using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayer stateData;

    protected bool turnImmediately;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;
    protected int amountOfTurnsDone;
    public LookForPlayerState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, D_LookForPlayer stateData) : base(enemyBase1, stateMachine, animatorBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemyBase1.CheckPlayerInMinAgroRange();
    }

        public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        enemyBase1.SetVelocity(0f);
    }

        public override void Exit()
    {
        base.Exit();
    }

        public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(turnImmediately)
        {
            enemyBase1.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if ( Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            enemyBase1.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

        public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public void SetTurnImmediately(bool flip)
    {
        turnImmediately = flip;
    }
}
