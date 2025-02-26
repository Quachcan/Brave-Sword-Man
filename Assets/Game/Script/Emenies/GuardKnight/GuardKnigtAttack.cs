using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardKnigtAttack : MonoBehaviour
{
    public float attackDamage = 50f;
    public float attackRadius = 4f;
    public float attackCoolDown = 2f;

    private float lastAttackTime;

    public bool canAttack => Time.time >= lastAttackTime + attackCoolDown;
    
    public Transform attackHitBoxPos;
    public LayerMask whatIsPlayer;

    public void TriggerAttack()
    {
            if(!canAttack) return;
            lastAttackTime = Time.time;
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsPlayer);

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
}

