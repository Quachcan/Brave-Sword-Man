using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, IDamageable
{
    public Player player;
    public float maxHealth = 200f;
    [SerializeField]
    private float currentHealth;
    private Animator animator;

    public void Initialize()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }


    public void Damage(float attackDamage)
    {
        currentHealth -= attackDamage;
        OnHit();
        animator.SetTrigger("hit");
        
        if(currentHealth <= 0)
        {
            player.Die();
            animator.SetTrigger("die");
        }
    }

    public void OnHit()
    {
        GameManager.Instance.OnPlayerHit();
    }

    public void OnDead()
    {

    }

}
