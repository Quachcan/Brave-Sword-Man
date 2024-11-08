using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;

    private PlayerBases playerBases;
    private EnemyBases enemyBases;
    public enum GameState
    {
        Playing,
        GameOver,
        Victory,
        Paused
    }

    public GameState currentGameState;
    

    private bool isPaused = false;    

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }



    void Start()
    {
        currentGameState = GameState.Playing;
        playerBases.Initialize();
        enemyBases.Initialize();
        
    }


    public void PauseGame()
    {
        if (currentGameState == GameState.Playing)
        {
            currentGameState = GameState.Paused;
            Time.timeScale = 0f;
            isPaused = true;
            // Todo: Hiển thị pause UI
            Debug.Log("Game Paused");
        }
    }

    public void UnpauseGame()
    {
        if (currentGameState == GameState.Paused)
        {
            currentGameState = GameState.Playing;
            Time.timeScale = 1f; 
            isPaused = false;

            Debug.Log("Game Unpaused");
            // Todo: HidePauseMenu();
        }
    }


    public bool IsGamePaused()
    {
        return isPaused;
    }

    public void DealDamage(IHit attacker, IHit target, float damageAmount)
    {
        target.TakeDamage(damageAmount);
        Debug.Log("Damage dealt: " + damageAmount);
    }

    public void OnPlayerDeath()
    {
        currentGameState = GameState.GameOver;
        Debug.Log("Game Over!");
    }

    public void OnEnemyDeath(EnemyBases enemy)
    {
        Debug.Log("Enemy has been defeated: " + enemy.gameObject.name);

    }
}
