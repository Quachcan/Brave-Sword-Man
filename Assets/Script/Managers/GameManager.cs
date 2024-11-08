using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Player player;
    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private EnemyBase1 enemyBase;

    private CinemachineVirtualCamera CVC;

    public static GameManager Instance { get; private set;}
    public GameState currentState { get; private set; } = GameState.Start;


    public enum GameState
{
    Start,
    Playing,
    Paused,
    GameOver
}

    private void Awake ()
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

    private void Start ()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        ChangeState(GameState.Start);
        player.Initialize();
        playerStat.Initialize();
        //enemyBase.Initialize();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentState == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (currentState == GameState.Paused)
                ChangeState(GameState.Playing);
        }

    }



    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.Start:
                StartGame();
                break;
            case GameState.Playing:
                ResumeGame();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

private void StartGame()
    {
        Debug.Log("Game Start");
    }

    private void ResumeGame()
    {
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void OnPlayerHit()
    {
        Debug.Log("Player is Hit");
    }
    //     public void PlayerDied()
    // {
    //     ChangeState(GameState.GameOver);
    // }

    public void OnPlayerDeath()
    {
        ChangeState(GameState.GameOver);
    }

    public void OnEnemyDeath(EnemyBase1 enemyBase)
    {
        throw new NotImplementedException();
    }

        public GameState GetCurrentState()
    {
        return currentState;
    }
}