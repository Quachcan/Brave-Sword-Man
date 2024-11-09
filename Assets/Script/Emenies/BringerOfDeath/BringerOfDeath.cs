using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BringerOfDeath : MonoBehaviour, IDamageable
{
    public Transform player;

    private Animator animator;

    public HealthBar healthBar;


    public float maxHealth = 500f;
    [SerializeField]
    private float currenHealth;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize()
    {
        healthBar.SetMaxHealth(maxHealth);
        currenHealth = maxHealth;
    }
    public void Flip()
    {
        if (player.position.x > transform.position.x)
        {
            transform.right = Vector3.left;
        }
        else
        {
            transform.right = Vector3.right;
        }
    }

    public void Damage(float damage)
    {
        currenHealth -= damage;
        healthBar.SetHealth(currenHealth);
        animator.SetTrigger("hit");
        OnHit();
        if (currenHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDead();
        animator.SetTrigger("Dead");
        StartCoroutine(DestroyAfterDelay(0.8f));
    }

    public void OnHit()
    {
        GameManager.Instance.BossTakeDamage();
    }

    public void OnDead()
    {
        GameManager.Instance.OnBossDeath();
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}



