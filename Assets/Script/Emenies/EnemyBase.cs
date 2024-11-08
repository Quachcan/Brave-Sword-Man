using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float maxHealth = 100;
    public float currentHealth;
    
    public float maxStamina = 100f;
    public float stamina;
    public float staminaRegenRate = 15f;
    public float moveSpeed = 3f;
    public float attackCoolDown = 2f;
    
    private bool canAttack;

    [Header("Attack Properties")]
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    
    [Header("Detection Properties")]
    public float detectionRange = 5f;
    public LayerMask detectionLayer;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        stamina = maxStamina;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        canAttack = true;
    }
        public virtual void TakeDamage()
    {
        currentHealth = maxHealth;
        stamina = maxStamina;
    }

    private void Update()
    {
        RegenerateStamina();
        DetectPlayer();
        DealDamageToPlayer();
    }

    private void DetectPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, detectionLayer);

                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    if ( distanceToPlayer > attackRange)
                    {
                        Vector2 newPos = (Vector2)transform.position + directionToPlayer * moveSpeed * Time.deltaTime;
                        rb.MovePosition(newPos);
                    }
                }
            }
        }
    }

    private void RegenerateStamina()
    {
        if (stamina < maxStamina)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
    }

    private void DealDamageToPlayer()
    {
        if (canAttack && player != null && stamina > 20f)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if(distance <= attackRange)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (player.TryGetComponent<PlayerBase>(out PlayerBase playerBase))
        {
            //GameManager.Instance.EnemyDealDamage(playerBase, attackDamage);
            stamina -= 20f;
            canAttack = false;
            StartCoroutine(AttackCoolDown());
        }
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }


    public void Damage(float attackDamage)
    {
            currentHealth -= attackDamage;

        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        //GameManager.Instance.OnEnemyDeath(this);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
