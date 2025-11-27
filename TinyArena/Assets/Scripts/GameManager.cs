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

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    void Update()
    {
        if (CurrentState == GameState.Paused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
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
                break;
            case GameState.GameOver:
                // Logic for the Game Over sequence
                HandleGameOverGameState();
                break;
        }
    }

    private void HandlePlayingGameState()
    {
        SceneManager.LoadScene("Scenes/TestScenes/JonasT/GameLevel");
    }

    private void HandleGameOverGameState()
    {
        SceneManager.LoadScene("Scenes/TestScenes/JonasT/GameOver");
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
