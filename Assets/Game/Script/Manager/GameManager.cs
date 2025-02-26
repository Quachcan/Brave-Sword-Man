using Game.Script.Player;
using UnityEngine;

namespace Game.Script.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public enum GameState {GameStart, Menu, Playing, Pause, GameOver}
        public GameState currentState;
        
        [Header("Managers")]
        public UIManager uiManager;
        public PlayerManager playerManager;
        public EnemyManager enemyManager;
        public AudioManager audioManager;
        public MapManager mapManager;
        public SceneLoader sceneLoader;
        
        public bool isRunning;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            Destroy(gameObject);
            InitializeAllScripts();
        }

        private void Start()
        {
            currentState = GameState.GameStart;
            isRunning = true;
        }

        private void InitializeAllScripts()
        {
            playerManager.Initialize();
        }

        public void GameStart()
        {
            isRunning = true;
            currentState = GameState.Menu;
            Debug.Log("Game Start");
        }

        public void GamePlaying()
        {
            isRunning = true;
            currentState = GameState.Playing;
        }

        public void PauseGame()
        {
            isRunning = false;
            currentState = GameState.Pause;
        }

        public void GameOver()
        {
            isRunning = false;
            currentState = GameState.GameOver;
        }
    }
}
