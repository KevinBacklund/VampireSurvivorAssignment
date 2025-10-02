using TMPro;
using UnityEngine;

public class GameManager : StateMachine
{
    public static GameManager Instance;
    public static bool gamePaused;
    public bool isGameover = false;
    public static bool choosingUpgrade = false;
    public static int currentScore;

    public SaveManager saveManager;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject levelUpScreen;
    public GameObject settingsScreen;
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentMoveSpeedDisplay;
    public TMP_Text currentDmgMultiplierDisplay;
    public TMP_Text currentLevelDisplay;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DisableScreens();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        saveManager.LoadData();
        SwitchState<GameplayState>();
    }

    private void Update()
    {
        UpdateStateMachine();
    }

    private void DisableScreens()
    {
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        levelUpScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        SwitchState<GameplayState>();
    }

    public void PauseMenu()
    {
        SwitchState<PausedState>();
    }

    public void Settings()
    {
        SwitchState<SettingsState>();
    }

    public void GameOver()
    {
        if (!isGameover)
        {
            SwitchState<GameOverState>();
        }
    }
}
