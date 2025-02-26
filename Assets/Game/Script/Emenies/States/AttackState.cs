using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;
    public AttackState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition) : base(enemyBase1, stateMachine, animatorBoolName)
    {
        this.attackPosition = attackPosition;
    }
    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = enemyBase1.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        enemyBase1.atsm.attackState = this;
        isAnimationFinished = false;
        enemyBase1.SetVelocity(0f);
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

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
