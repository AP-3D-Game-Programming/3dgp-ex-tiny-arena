using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public delegate void GameStateChanged(GameState newState);
    public event GameStateChanged OnGameStateChanged;

    private GameState currentState;

    public GameState CurrentState
    {
        get => currentState;
        private set
        {
            if (currentState != value)
            {
                currentState = value;
                OnGameStateChanged?.Invoke(currentState);
            }
        }
    }

    public string gameplaySceneName { get; set; }

    private GameObject gameOverUI;
    private Canvas gameOverUICanvas;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    void Start()
    {
        ChangeState(GameState.Playing);
    }

    void Update()
    {
        if (gameOverUI is not null && gameOverUICanvas is not null) return;
        else if (gameOverUI is null && gameOverUICanvas is null)
        {
            gameOverUI = GameObject.FindGameObjectWithTag("GameOver");
            //DontDestroyOnLoad(gameOverUI);
            return;
        }
        else if (gameOverUI is not null && gameOverUICanvas is null)
        {
            gameOverUICanvas = gameOverUI.GetComponent<Canvas>();
            gameOverUICanvas.enabled = false;
            //DontDestroyOnLoad(gameOverUICanvas);
            return;
        }
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                // Logic for entering the Main Menu
                break;
            case GameState.Playing:
                // Logic for starting Gameplay
                HandlePlayingGameState();
                break;
            case GameState.Paused:
                // Logic for Pausing the game
                HandlePausedGameState();
                break;
            case GameState.GameOver:
                // Logic for the Game Over sequence
                HandleGameOverGameState();
                break;
        }
    }

    private async void HandlePlayingGameState()
    {
        if (SceneManager.GetSceneByName("GameLevelScene").isLoaded)
        {
            await SceneManager.UnloadSceneAsync("GameLevelScene");
            await Resources.UnloadUnusedAssets();
        }

        await SceneManager.LoadSceneAsync("GameLevelScene", LoadSceneMode.Additive);

        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        
        if (gameOverUICanvas is not null)
            gameOverUICanvas.enabled = false;
    }

    private void HandlePausedGameState()
    {
        Time.timeScale = 0.0f;
    }

    private void HandleGameOverGameState()
    {
        if (gameOverUICanvas is null)
            Debug.LogError("GameOverCanvas could not be found.");
        else
        {
            gameOverUICanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
