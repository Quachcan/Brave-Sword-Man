using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage = 50f;
    public float attackRadius = 4f;
    
    public Transform attackHitBoxPos;
    public LayerMask whatIsPlayer;

    public void TriggerAttack()
    {
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
