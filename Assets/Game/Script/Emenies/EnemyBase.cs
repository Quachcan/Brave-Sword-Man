using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Enemy Stats")]
    public float maxHealth = 100;
    public float currentHealth;
    

    [Header("Attack Properties")]
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    

    private Transform player;
    private Rigidbody2D rb;
    public HealthBar healthBar;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
    }


    public void Damage(float attackDamage)
    {
        currentHealth -= attackDamage;
        healthBar.SetHealth(currentHealth);
        OnHit();

        if (currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDead();
        Destroy(gameObject);
    }

    public void OnHit()
    {
        OldGameManager.Instance.OnEnemyHit();
    }

    public void OnDead()
    {
        OldGameManager.Instance.OnEnemyDeath(this);
    }
}
