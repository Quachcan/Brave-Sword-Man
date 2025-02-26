using UnityEngine;

public class EnemyBases : MonoBehaviour, IHit, Initializeable
{
    public float maxHealth = 100f;
    public float currentHealth;

    private GameManagers gameManagers;
    public void Initialize()
    {
        currentHealth = maxHealth;
        gameManagers = GameManagers.instance;
    }

    // Phương thức nhận sát thương
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Enemy took damage: " + damageAmount + ", Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Trả về Transform của Enemy
    public Transform GetTransform()
    {
        return transform;
    }

    private void Die()
    {
        GameManagers.instance.OnEnemyDeath(this);
        Debug.Log("Enemy has died");
        Destroy(gameObject);
    }

}