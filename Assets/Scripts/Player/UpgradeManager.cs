using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<StatUpgrade> statUpgradeSlots = new List<StatUpgrade>(6); 
    public int[] statUpgradeLevels = new int[6];

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class StatUp
    {
        public int StatUpIndex;
        public GameObject initialStatUpgrade;
        public StatUpgradeScriptableObject statUpgradeData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TMP_Text upgradeNameDisplay;
        public TMP_Text upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<StatUp> statUpgradeOptions = new List<StatUp>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerStats player;


    private void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
    }

    public void AddStatUpgrade(int slotIndex, StatUpgrade statUpgrade)
    {
        statUpgradeSlots[slotIndex] = statUpgrade;
        statUpgradeLevels[slotIndex] = statUpgrade.statUpgradeData.Level;
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent <WeaponController>().weaponData.Level;

            weaponUpgradeOptions[upgradeIndex].weaponData = upgradedWeapon.GetComponent<WeaponController>().weaponData;


            GameManager.choosingUpgrade = false;
        } 
    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<StatUp> availableStatUpgrade = new List<StatUp>(statUpgradeOptions);


        foreach (var upgradeOption in upgradeUIOptions)
        {
            if(availableWeaponUpgrades.Count == 0 && availableStatUpgrade.Count == 0)
            {
                return;
            }

            int upgradeType;

            if(availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else if (availableStatUpgrade.Count == 0)
            {
                upgradeType = 0;
            }
            else
            {
                upgradeType = Random.Range(0, 2);
            }

            if (upgradeType == 0)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];

                availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                if (chosenWeaponUpgrade != null)
                {
                    bool newWeapon = false;
                    for (int i = 0; i < weaponUpgradeOptions.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
                                {
                                    upgradeType = 1;
                                    break;
                                }


                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;
                                upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
                        upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                    }

                    
                }
            }

            if (upgradeType == 1)
            {
                StatUp chosenStatUpgrade = availableStatUpgrade[Random.Range(0, availableStatUpgrade.Count)];

                availableStatUpgrade.Remove(chosenStatUpgrade);

                if (chosenStatUpgrade != null)
                {
                    upgradeOption.upgradeButton.onClick.AddListener(() => player.StatUpgrade(chosenStatUpgrade.statUpgradeData.Name, chosenStatUpgrade.statUpgradeData.Multiplier));
                    upgradeOption.upgradeDescriptionDisplay.text = chosenStatUpgrade.statUpgradeData.Description;
                    upgradeOption.upgradeNameDisplay.text = chosenStatUpgrade.statUpgradeData.Name;
                    upgradeOption.upgradeIcon.sprite = chosenStatUpgrade.statUpgradeData.Icon;
                }
            }
        }
    }

    void RemoveUpgradeOptions()
    {
        foreach(var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }
}
