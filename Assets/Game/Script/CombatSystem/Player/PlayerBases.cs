using UnityEngine;

public class PlayerBases : MonoBehaviour, IHit
{
    public float maxHealth = 100f;
    public float currentHealth;
    private GameManagers gameManagers;

    public void Initialize()
    {
        currentHealth = maxHealth;
        gameManagers = GameManagers.instance;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took damage: " + damageAmount + ", Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Die()
    {
        GameManagers.instance.OnPlayerDeath();
        Debug.Log("Player has died");
        Destroy(gameObject);
    }
}