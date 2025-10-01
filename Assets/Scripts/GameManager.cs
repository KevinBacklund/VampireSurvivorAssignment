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
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentMoveSpeedDisplay;
    public TMP_Text currentDmgMultiplierDisplay;
    public TMP_Text currentLevelDisplay;


    private void Awake()
    {
        Instance = this;
        DisableScreens();
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
    }

    public void ResumeGame()
    {
        SwitchState<GameplayState>();
    }

    public void GameOver()
    {
        if (!isGameover)
        {
            SwitchState<GameOverState>();
        }
    }
}
