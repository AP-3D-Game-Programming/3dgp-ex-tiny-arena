using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject mainMenuUI;

    private GameObject pauseMenuUI;
    private Canvas pauseMenuUICanvas;

    private GameObject settingsMenuUI;
    private Canvas settingsMenuUICanvas;

    private GameObject gameOverUI;
    private Canvas gameOverUICanvas;

    private GameObject player;
    private PlayerHealth playerHealth;

    private Scene currentScene;

    private Button startButton;
    private Button continueButton;
    private Button settingsButton;
    private Button quitButton;
    private Button playAgainButton;

    private Slider sfxVolumeSlider;
    private Slider musicVolumeSlider;

    [Header("Audio")]
    public AudioClip menuMusic;

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
            case GameState.Settings:
                HandleSettingsGameState();
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
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(menuMusic);
        }

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
        if (continueButton != null)
            continueButton.onClick.RemoveListener(ContinueGameplayOnClick);

        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(LaunchSettingsOnClick);

        if (quitButton != null)
            quitButton.onClick.RemoveListener(QuitGameOnClick);

        if (playAgainButton != null)
            playAgainButton.onClick.RemoveListener(ResetPlayingStateOnClick);

        if (SceneManager.GetSceneByName("GameLevelScene").isLoaded)
        {
            currentScene = SceneManager.GetSceneByName("GameLevelScene");
            player = GameObject.FindGameObjectWithTag("Player");

            pauseMenuUI = GameObject.FindGameObjectWithTag("PauseMenu");
            pauseMenuUICanvas = pauseMenuUI.GetComponent<Canvas>();

            settingsMenuUI = GameObject.FindGameObjectWithTag("SettingsMenu");
            settingsMenuUICanvas = settingsMenuUI.GetComponent<Canvas>();

            gameOverUI = GameObject.FindGameObjectWithTag("GameOver");
            gameOverUICanvas = gameOverUI.GetComponent<Canvas>();

            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;

            pauseMenuUICanvas.enabled = false;
            settingsMenuUICanvas.enabled = false;
            gameOverUICanvas.enabled = false;

            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                if (playerHealth.health <= 0 && CurrentState != GameState.GameOver) ChangeState(GameState.GameOver);

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                ChangeState(GameState.Paused);
        }
        else SceneManager.LoadScene("GameLevelScene", LoadSceneMode.Additive);
    }

    private void HandlePausedGameState()
    {
        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(LaunchSettingsOnClick);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.RemoveListener(ChangeSfxVolumeOnSlide);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.RemoveListener(ChangeMusicVolumeOnSlide);

        settingsMenuUICanvas.enabled = false;

        if (pauseMenuUI != null && pauseMenuUICanvas != null)
        {
            Button[] buttons = pauseMenuUI.transform.Find("Menu").GetComponentsInChildren<Button>();
            continueButton = buttons.Where(button => button.name == "ContinueButton").FirstOrDefault();
            settingsButton = buttons.Where(button => button.name == "SettingsButton").FirstOrDefault();
            quitButton = buttons.Where(button => button.name == "QuitButton").FirstOrDefault();

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                ChangeState(GameState.Playing);

            if (continueButton != null)
                continueButton.onClick.AddListener(ContinueGameplayOnClick);

            if (settingsButton != null)
                settingsButton.onClick.AddListener(LaunchSettingsOnClick);

            if (quitButton != null)
                quitButton.onClick.AddListener(QuitGameOnClick);

            pauseMenuUICanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
        else Debug.LogError(nameof(pauseMenuUICanvas) + " could not be found.");
    }

    private void HandleSettingsGameState()
    {
        if (settingsMenuUI != null && settingsMenuUICanvas != null)
        {
            Slider[] sliders = settingsMenuUI.transform.Find("Menu").GetComponentsInChildren<Slider>();
            sfxVolumeSlider = sliders.Where(slider => slider.name == "SfxVolumeSlider").FirstOrDefault();
            musicVolumeSlider = sliders.Where(slider => slider.name == "MusicVolumeSlider").FirstOrDefault();

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                ChangeState(GameState.Paused);

            if (sfxVolumeSlider != null)
                sfxVolumeSlider.onValueChanged.AddListener(ChangeSfxVolumeOnSlide);

            if (musicVolumeSlider != null)
                musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolumeOnSlide);

            pauseMenuUICanvas.enabled = false;
            settingsMenuUICanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
        }
        else Debug.LogError(nameof(settingsMenuUICanvas) + " could not be found.");
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
        else Debug.LogError(nameof(gameOverUICanvas) + " could not be found.");
    }

    private void StartGameOnClick()
    {
        ChangeState(GameState.Playing);
        SceneManager.UnloadSceneAsync("MainMenuScene");
    }

    private void ContinueGameplayOnClick()
    {
        ChangeState(GameState.Playing);
    }

    private void LaunchSettingsOnClick()
    {
        ChangeState(GameState.Settings);
    }

    private void ChangeSfxVolumeOnSlide(float value)
    {
        AudioManager.Instance.sfxSource.volume = value;
    }

    private void ChangeMusicVolumeOnSlide(float value)
    {
        AudioManager.Instance.musicSource.volume = value;
    }

    private void QuitGameOnClick()
    {
        Application.Quit();
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
    Settings,
    GameOver
}
