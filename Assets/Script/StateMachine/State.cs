using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected EnemyBase1 enemyBase1;

    protected float startTime;

    protected string animatorBoolName;

    public State(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName)
    {
        this.stateMachine = stateMachine;
        this.enemyBase1 = enemyBase1;
        this.animatorBoolName = animatorBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        enemyBase1.animator.SetBool(animatorBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        if (enemyBase1 != null)
        {

        enemyBase1.animator?.SetBool(animatorBoolName, false);
        }
        else
        {
            return;
        }
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
    }
}
