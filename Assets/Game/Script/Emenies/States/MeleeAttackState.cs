using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{

    public float attackDamage = 15f;
    protected D_MeleeAttack stateData;

    protected AttackDetails attackDetails;
    public MeleeAttackState(EnemyBase1 enemyBase1, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(enemyBase1, stateMachine, animatorBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = enemyBase1.aliveGO.transform.position; 
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            PlayerStat player = collider.GetComponent<PlayerStat>();
            if(player != null)
            {
                player.Damage(attackDamage);
            }
            else
            {
                Debug.Log("Damage is not call");
            }
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
