using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCombat2 : PlayerBase
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius;
    [SerializeField]
    private int attack1Damage;
    [SerializeField]
    private float staminaCosPerAttack = 10f;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    private bool gotInput;
    private bool isAttacking, isFirstAttack;


    private float lastInputTime = Mathf.NegativeInfinity;

    private AttackDetails attackDetails; 

    private Animator animator;

    private PlayerController3 PC;
    private PlayerBase PB;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PC = GetComponent<PlayerController3>();
        animator.SetBool("canAttack", combatEnabled);
        PB = GetComponent<PlayerBase>();
    }


    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetButtonDown("Fire1") && PC.stamina >= staminaCosPerAttack)
        {
            if (combatEnabled)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
                PC.stamina -= staminaCosPerAttack;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                animator.SetBool("attack1", true);
                animator.SetBool("firstAttack", isFirstAttack);
                animator.SetBool("isAttacking", isAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage; 
        attackDetails.position = transform.position;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("attack1", false);
    }

    private new void Damage(AttackDetails attackDetails)
    {
        if(!PC.GetDashStatus())
        {
            int direction;

            //PB.TakeDamage(attackDetails.damageAmount);
            
            if (attackDetails.position.x < transform.position.x)
            {
                direction = 1;
            }
            else 
            {
                direction = -1;
            }

            PC.KnockBack(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
