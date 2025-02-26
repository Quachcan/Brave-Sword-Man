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
        OldUIManager.Instance.InitializeHealthBar(playerHealthBar);
    }

    private void UpdateGameManagerComponents()
    {
        OldGameManager oldGameManager = OldGameManager.Instance;
        
        if (oldGameManager != null)
        {
            oldGameManager.player = FindObjectOfType<Player>();
            oldGameManager.playerStat = FindObjectOfType<PlayerStat>();
            oldGameManager.enemyBase = FindObjectOfType<EnemyBase>();
            oldGameManager.bringerOfDeath = FindObjectOfType<BringerOfDeath>();

            oldGameManager.playerStat?.Initialize();
            oldGameManager.bringerOfDeath?.Initialize();
            oldGameManager.guardKnight?.Initialize();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}