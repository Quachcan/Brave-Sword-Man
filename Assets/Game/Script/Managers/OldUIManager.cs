using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldUIManager : MonoBehaviour
{

    public GameObject bnGameOver;
    public static OldUIManager Instance { get; private set; }

    [SerializeField]
    public HealthBar playerHealthBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    public void InitializeHealthBar(HealthBar playerHealth)
    {
        playerHealthBar = playerHealth;
        bnGameOver.SetActive(false);
    }

    public void SetPlayerMaxHealth(float maxHealth)
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.SetMaxHealth(maxHealth);
        }
    }

    public void UpdatePlayerHealth(float currentHealth)
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.SetHealth(currentHealth);
        }
    }

    public void GameOverUI()
    {
        bnGameOver.SetActive(true);
    }
}