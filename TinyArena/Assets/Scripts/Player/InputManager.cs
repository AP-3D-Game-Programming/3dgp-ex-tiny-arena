using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTestManager : MonoBehaviour
{
    private GameObject gameOverUI;
    private Canvas gameOverUICanvas;
    private Scene currentScene;
    private GameObject player;
    private PlayerHealth playerHealth;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerShoot shoot;
    private SpellManager spellManager;

    private void Awake()
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
    public string gameplaySceneName { get; set; }

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
        ChangeState(GameState.Playing);

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        shoot = GetComponent<PlayerShoot>();
        spellManager = GetComponent<SpellManager>();

        // Movement
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Sprint.canceled += ctx => motor.Walk();

        // Shooting
        onFoot.Shoot.performed += ctx => shoot.StartFiring();
        onFoot.Shoot.canceled += ctx => shoot.StopFiring();

        // Switch spells
        onFoot.ChangeWeapon.performed += ctx => spellManager.NextSpell();
    }

    private void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
    }

    private void HandlePlayingGameState()
    {
        if (SceneManager.GetSceneByName("GameLevelScene").isLoaded)
        {
            currentScene = SceneManager.GetSceneByName("GameLevelScene");
            Cursor.lockState = CursorLockMode.Locked;
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (player.health <= 0 )
            }
        }
        SceneManager.LoadScene("GameLevelScene", LoadSceneMode.Additive);
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
