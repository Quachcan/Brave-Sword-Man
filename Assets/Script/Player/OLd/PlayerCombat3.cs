using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float attackRadius = 0.5f; // Bán kính đòn tấn công
    [SerializeField] private int attackDamage = 10;     // Sát thương đòn tấn công
    [SerializeField] private Transform attackHitBoxPos; // Vị trí hitbox tấn công
    [SerializeField] private LayerMask damageableLayer; // Layer của đối tượng có thể bị gây sát thương

    private PlayerBase playerBase;
    private Animator animator;
    private bool isAttacking;

    private void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (GameManager.Instance.GetCurrentState() == GameManager.GameState.Playing)
        {
            HandleAttackInput();
        }
    }

    private void HandleAttackInput()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); 
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, damageableLayer);

        foreach (Collider2D collider in detectedObjects)
        {

            if (collider.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                //GameManager.Instance.PlayerDealDamage(enemy, attackDamage);
            }
        }


        StartCoroutine(ResetAttackCooldown());
    }


    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (attackHitBoxPos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
        }
    }
}