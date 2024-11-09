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

    public void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }


    public void Damage(float attackDamage)
    {
        currentHealth -= attackDamage;
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
        GameManager.Instance.OnEnemyHit();
    }

    public void OnDead()
    {
        GameManager.Instance.OnEnemyDeath(this);
    }
}
