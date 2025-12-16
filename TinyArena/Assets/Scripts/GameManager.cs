using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject mainMenuUI;
    private GameObject gameOverUI;
    private Canvas gameOverUICanvas;
    private GameObject player;
    private PlayerHealth playerHealth;
    private Scene currentScene;
    private Button startButton;
    private Button playAgainButton;

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

    public static GameManager Instance;

    public delegate void GameStateChanged(GameState newState);
    public event GameStateChanged OnGameStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                HandleMainMenuGameState();
                break;
            case GameState.Playing:
                HandlePlayingGameState();
                break;
            case GameState.Paused:
                HandlePausedGameState();
                break;
            case GameState.GameOver:
                HandleGameOverGameState();
                break;
        }
    }

    private void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
    }

    private void HandleMainMenuGameState()
    {
        if (SceneManager.GetSceneByName("MainMenuScene").isLoaded)
        {
            currentScene = SceneManager.GetSceneByName("MainMenuScene");

            if (mainMenuUI == null)
                mainMenuUI = GameObject.FindGameObjectWithTag("MainMenu");

            if (startButton == null)
                startButton = mainMenuUI.GetComponentInChildren<Button>();

            if (startButton != null)
            {
                startButton.onClick.RemoveAllListeners();
                startButton.onClick.AddListener(StartGameOnClick);
            }
        }
        else SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Additive);
    }

    private void HandlePlayingGameState()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.RemoveListener(ResetPlayingStateOnClick);

        if (SceneManager.GetSceneByName("GameLevelScene").isLoaded)
        {
            currentScene = SceneManager.GetSceneByName("GameLevelScene");
            player = GameObject.FindGameObjectWithTag("Player");

            gameOverUI = GameObject.FindGameObjectWithTag("GameOver");
            gameOverUICanvas = gameOverUI.GetComponent<Canvas>();

            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
            gameOverUICanvas.enabled = false;

            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                if (playerHealth.health <= 0 && CurrentState != GameState.GameOver) ChangeState(GameState.GameOver);
        }
        else SceneManager.LoadScene("GameLevelScene", LoadSceneMode.Additive);
    }

    private void HandlePausedGameState()
    {
        Time.timeScale = 0.0f;
    }

    private void HandleGameOverGameState()
    {
        if (gameOverUI != null && gameOverUICanvas != null)
        {
            playAgainButton = gameOverUI.GetComponentInChildren<Button>();

            if (playAgainButton != null)
                playAgainButton.onClick.AddListener(ResetPlayingStateOnClick);

            gameOverUICanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
        else Debug.LogError("GameOverCanvas could not be found.");
    }

    private void StartGameOnClick()
    {
        ChangeState(GameState.Playing);
        SceneManager.UnloadSceneAsync("MainMenuScene");
    }

    private void ResetPlayingStateOnClick()
    {
        ChangeState(GameState.Playing);
        playerHealth.restoreHealth(playerHealth.maxHealth);
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
