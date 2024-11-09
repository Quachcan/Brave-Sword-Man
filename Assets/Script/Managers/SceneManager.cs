using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneComponentManager : MonoBehaviour
{
    public static SceneComponentManager Instance { get; private set; }

    private HealthBar playerHealthBar;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateGameManagerComponents();
        playerHealthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
        UIManager.Instance.InitializeHealthBar(playerHealthBar);
    }

    private void UpdateGameManagerComponents()
    {
        GameManager gameManager = GameManager.Instance;
        
        if (gameManager != null)
        {
            gameManager.player = FindObjectOfType<Player>();
            gameManager.playerStat = FindObjectOfType<PlayerStat>();
            gameManager.enemyBase = FindObjectOfType<EnemyBase>();
            gameManager.bringerOfDeath = FindObjectOfType<BringerOfDeath>();

            gameManager.playerStat?.Initialize();
            gameManager.bringerOfDeath?.Initialize();
            gameManager.guardKnight?.Initialize();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}