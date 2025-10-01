using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public CharacterSriptableObject characterData;

    [HideInInspector]
    float currentHealth;
    [HideInInspector]
    float currentRecovery;
    [HideInInspector]
    float currentMoveSpeed;
    [HideInInspector]
    float currentDmgMultiplier;

    public List<GameObject> spawnedWeapons;


    [Header("UI")]
    public Image healthBar;
    public Image xpBar;

    [Header("Debug")]
    public bool isInvincible;

    public float CurrentHealth
    {
        get {  return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                GameManager.Instance.currentHealthDisplay.text = "Health: " + (int)currentHealth + "/" + characterData.MaxHealth;
            }
        }
    }
    
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                GameManager.Instance.currentRecoveryDisplay.text = "Recovery: x" + currentRecovery;
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: x" + currentMoveSpeed;
            }
        }
    }

    public float CurrentDmgMultiplier
    {
        get { return currentDmgMultiplier; }
        set
        {
            if (currentDmgMultiplier != value)
            {
                currentDmgMultiplier = value;
                GameManager.Instance.currentDmgMultiplierDisplay.text = "Dmg Multiplier: x" + currentDmgMultiplier;
            }
        }
    }

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    UpgradeManager upgradeManager;
    public int weaponIndex;
    public int statUpgradeIndex;



    private void Awake()
    {
        upgradeManager = GetComponent<UpgradeManager>();

        currentHealth = characterData.MaxHealth;
        currentDmgMultiplier = characterData.DmgMultiplier;
        currentMoveSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;

        SpawnWeapon(characterData.StartingWeapon);
    }



    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        GameManager.Instance.currentHealthDisplay.text = "Health: " + +(int)currentHealth + "/" + characterData.MaxHealth;
        GameManager.Instance.currentRecoveryDisplay.text = "Recovery: x" + currentRecovery;
        GameManager.Instance.currentMoveSpeedDisplay.text = "Move Speed: x" + currentMoveSpeed;
        GameManager.Instance.currentDmgMultiplierDisplay.text = "Dmg Multiplier: x" + currentDmgMultiplier;
        GameManager.Instance.currentLevelDisplay.text = "Level: " + level;

        UpdateHealthBar();
        UpdateXpBar();
    }


    private void Update()
    {
        if (!GameManager.gamePaused)
        {
            Recover();
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();

        UpdateXpBar();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            GameManager.Instance.currentLevelDisplay.text = "Level: " + level;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            SoundManager.Playsound(SoundType.levelUp);

            GameManager.Instance.SwitchState<LevelUpState>();
                
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;
        }

        if (CurrentHealth <= 0)
        {
            Kill();
        }

        UpdateHealthBar();
    }

    void Kill()
    {
        if (!GameManager.Instance.isGameover)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    void UpdateXpBar()
    {
        xpBar.fillAmount = (float)experience / experienceCap;
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= upgradeManager.weaponSlots.Count - 1)
        {
            Debug.LogError("Weapon slots full");
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        upgradeManager.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
        GameManager.choosingUpgrade = false;
    }

    public void SpawnStatUpgrade(GameObject statUpgrade)
    {
        if (statUpgradeIndex >= upgradeManager.statUpgradeSlots.Count - 1)
        {
            Debug.LogError("statUpgrade slots full");
            return;
        }

        GameObject spawnedStatUpgrade = Instantiate(statUpgrade, transform.position, Quaternion.identity);
        spawnedStatUpgrade.transform.SetParent(transform);
        upgradeManager.AddStatUpgrade(statUpgradeIndex, spawnedStatUpgrade.GetComponent<StatUpgrade>());

        statUpgradeIndex++;
        GameManager.choosingUpgrade = false;
    }

    public void StatUpgrade(string statToUpgrade)
    {

    }
}
