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
    [HideInInspector]
    float currentMaxHealth;

    public List<GameObject> spawnedWeapons;


    [Header("UI")]
    public Image healthBar;
    public Image xpBar;

    [Header("Debug")]
    public bool isInvincible;

    public float CurrentMaxHealth
    {
        get { return currentMaxHealth; }
        set
        {
            if (characterData.MaxHealth != value)
            {
                currentMaxHealth = value;
                GameManager.Instance.currentHealthDisplay.text = "Health: " + (int)currentHealth + "/" + currentMaxHealth;
            }
        }
    }

    public float CurrentHealth
    {
        get {  return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                GameManager.Instance.currentHealthDisplay.text = "Health: " + (int)currentHealth + "/" + currentMaxHealth;
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

        currentMaxHealth = characterData.MaxHealth;
        currentHealth = characterData.MaxHealth;
        currentDmgMultiplier = characterData.DmgMultiplier;
        currentMoveSpeed = characterData.MoveSpeed;
        currentRecovery = characterData.Recovery;

        SpawnWeapon(characterData.StartingWeapon);
    }



    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        GameManager.Instance.currentHealthDisplay.text = "Health: " + +(int)currentHealth + "/" + currentMaxHealth;
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
        SoundManager.PlaySfx(SfxType.expGain);
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
            SoundManager.PlaySfx(SfxType.levelUp);

            GameManager.Instance.SwitchState<LevelUpState>();
                
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;
            SoundManager.PlaySfx(SfxType.playerDamaged);
        }

        if (CurrentHealth <= 0)
        {
            Kill();
        }

        UpdateHealthBar();
    }

    void Kill()
    {
        SoundManager.PlaySfx(SfxType.playerDeath);
        if (!GameManager.Instance.isGameover)
        {
            GameManager.Instance.GameOver();
        }
    }

    void Recover()
    {
        if (CurrentHealth < currentMaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if(CurrentHealth > currentMaxHealth)
            {
                CurrentHealth = currentMaxHealth;
            }
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / currentMaxHealth;
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

    /*public void SpawnStatUpgrade(GameObject statUpgrade)
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
    }*/

    public void StatUpgrade(string statToUpgrade, float multiplier)      
    {
        multiplier = 1 + 0.01f * multiplier;
        if (statToUpgrade == "MaxHealth")
        {
            currentMaxHealth *= multiplier;
            UpdateHealthBar();
            GameManager.Instance.currentHealthDisplay.text = "Health: " + (int)currentHealth + "/" + currentMaxHealth;
        }
        else if (statToUpgrade == "Recovery")
        {
            CurrentRecovery *= multiplier;
        }
        else if (statToUpgrade == "MoveSpeed")
        {
            CurrentMoveSpeed *= multiplier;
        }
        else if (statToUpgrade == "DmgMultiplier")
        {
            CurrentDmgMultiplier *= multiplier;
        }
        else
        {
            Debug.LogError("Invalid stat to upgrade: " + statToUpgrade);
        }
        GameManager.choosingUpgrade = false;
    }
}
