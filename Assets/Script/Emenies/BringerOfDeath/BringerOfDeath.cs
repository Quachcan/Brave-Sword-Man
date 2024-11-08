using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath : MonoBehaviour
{
    [SerializeField]
    private Transform attackHitBoxPos;
    [SerializeField]
    private float attackHitBoxRadius = 3f;
    public float moveSpeed = 3f;
    public float maxHealth = 1000f;
    public float maxStamina = 150f;
    public float attackRange = 3f;
    public float attackCoolDown = 2f;
    public float staminaRegenRate = 20f;
    public float enhanceMoveSpeed = 1.5f;
    
    public int baseDamage = 20;
    public int enhancedDamage = 2;
    
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float stamina;
    
    private bool canAttack;
    private bool isEnraged;

    [Header("Detection Properties")]
    public float detectionRange = 10f;
    public LayerMask detectionLayer;


    private Transform player; 
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private LayerMask whatIsPlayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        stamina = maxStamina;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        DealDamageToPlayer();
        RegenerateStamina();
        if (currentHealth < maxHealth / 3 && !isEnraged)
        {
            CheckEnragedState();
        }
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
                        Vector2 targetPosition = new Vector2(player.position.x, rb.position.y);
                        //transform.position = Vector2.MoveTowards(transform.position, directionToPlayer, moveSpeed * Time.deltaTime);
                        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
                        rb.MovePosition(newPos);
                    }

                    if (player.position.x > transform.position.x)
                    {
                        transform.right = Vector3.left;
                    }
                    else
                    {
                        transform.right = Vector3.right;
                    }
                    }
            }
        }
    }

    private void CheckEnragedState()
    {
        if ( !isEnraged && currentHealth < maxHealth / 3)
        {
            isEnraged = true;
            moveSpeed *= enhanceMoveSpeed;
            baseDamage *= enhancedDamage;
        }
    }

    private void RegenerateStamina()
    {
        if ( stamina < 0)
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
        }
    }

    private void DealDamageToPlayer()
    {
        if (canAttack && player != null && stamina > 30f)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if(distance <= attackRange)
            {
                Attack();
            }
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectHitBox = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsPlayer);

        foreach (Collider2D collider in detectHitBox)
        {
            if ( collider.TryGetComponent<PlayerBase>(out PlayerBase player))
            {
                //GameManager.Instance.EnemyDealDamage(player, baseDamage);
            }
        }    
    }

    public void Attack()
    {
        if (player.TryGetComponent<PlayerBase>(out PlayerBase playerBase))
        {   
            //GameManager.Instance.EnemyDealDamage(playerBase, baseDamage);
            stamina -= 30f;
            canAttack = false;
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCoolDown());
        }
    }

private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth < 0)
        {
            animator.SetTrigger("Dead");
            Die();
        }
    }

    public void Die()
    {
        //GameManager.Instance.OnEnemyDeath(this);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackHitBoxRadius);
    }
}
