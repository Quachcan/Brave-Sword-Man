using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamageable
{
    public Player player;
    public float maxHealth = 200f;
    [SerializeField]
    private float currentHealth;
    [SerializeField]    
    private HealthBar healthBar;
    private Animator animator;

    public void Initialize()
    {
        player = GetComponent<Player>();
        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
    }


    public void Damage(float attackDamage)
    {
        currentHealth -= attackDamage;
        OnHit();
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("hit");
        
        if(currentHealth <= 0)
        {
            player.Die();
            animator.SetTrigger("die");
        }
    }

    public void OnHit()
    {
        OldGameManager.Instance.OnPlayerHit();
    }

    public void OnDead()
    {

    }

}
