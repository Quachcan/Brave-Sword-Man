using UnityEngine;

public class PlayerCombats : MonoBehaviour
{
    public float attackDamage = 30f;
    public float attackRadius = 1.5f;  

    public Transform attackHitBoxPos;  
    public LayerMask whatIsDamageable;

    private Animator animator;
    private GameManager gameManager;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            CheckAttackHitBox();
            animator.SetTrigger("attack1");
        }
    }

    private void CheckAttackHitBox()
    {

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            EnemyBase enemy = collider.GetComponent<EnemyBase>();
            if(enemy != null)
            {
                enemy.Damage(attackDamage);
            }
            else
            {
                Debug.Log("Damage is not call");
            }

            BringerOfDeath bringerOfDeath = collider.GetComponent<BringerOfDeath>();
            if (bringerOfDeath != null)
            {
                bringerOfDeath.Damage(attackDamage);
            }
            else
            {
                Debug.Log("Damage is not call");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }
}