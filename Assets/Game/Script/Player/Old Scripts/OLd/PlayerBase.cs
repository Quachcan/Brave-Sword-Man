using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float maxHealth = 100;
    public float  currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRegenRate = 10f;
    [SerializeField]
    private OldGameManager GM;

    public delegate void OnHealthChange(float currenthealth);
    public event OnHealthChange onHealthChange;
    
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath onPlayerDeath;

    public void Initialize()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    void Update()
    {
        RegenerateStamina();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        onHealthChange?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            //Die();
        }
    }
    internal void Damage(AttackDetails attackDetails)
    {
        float damageAmount = attackDetails.damageAmount;
        TakeDamage(damageAmount);
    }

    public void EnemyDealDamage(PlayerBase player, int damageAmount)
    {
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }

    public void RegenerateStamina()
    {
        if (currentHealth < maxHealth)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
    }


}
