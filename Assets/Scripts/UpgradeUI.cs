using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_attackPowerValue;
    [SerializeField] private TextMeshProUGUI m_attackSpeedValue;
    
    [SerializeField] private TextMeshProUGUI m_attackPowerLevel;
    [SerializeField] private TextMeshProUGUI m_attackSpeedLevel;
    
    [SerializeField] private TextMeshProUGUI m_attackPowerCost;
    [SerializeField] private TextMeshProUGUI m_attackSpeedCost;
    
    [SerializeField] private Button m_backBtn;
    
    [SerializeField] private Button m_upgradeSpeedBtn;
    [SerializeField] private Button m_upgradePowerBtn;
    
    [SerializeField] private TextMeshProUGUI m_name;

    [SerializeField] private TextMeshProUGUI m_currentPointsText;
    
    [SerializeField] private Button m_switchBtn;
    
    
    float m_upgradePowerCost; 
    float m_upgradeSpeedCost;
    

    void Start()
    {
        UpdateStats();
        UpdatePoints();
        m_upgradeSpeedBtn.onClick.AddListener(() => OnClickUpgradeSpeed());
        m_upgradePowerBtn.onClick.AddListener(() => OnClickUpgradePower());
        m_backBtn.onClick.AddListener(() => OnClickBack());
        m_switchBtn.onClick.AddListener(() => SwitchCharacter());
   
      
    }

    void OnClickUpgradeSpeed()
    {
        Profile.SubtractPoints(m_upgradeSpeedCost);
        PlayerDataManager.Current.UpgradeSpeed();
        UpdateStats();
        UpdatePoints();
    }
    
    void OnClickUpgradePower()
    {
        Profile.SubtractPoints(m_upgradePowerCost);
        PlayerDataManager.Current.UpgradePower();
        UpdateStats();
        UpdatePoints();
    }

    void UpdateStats()
    {
     
        PlayerData playerData = PlayerDataManager.Current.GetPlayerData();
        float currentPoints = Profile.GetPoints();

         m_upgradePowerCost = GetUpgradePowerCost(playerData.currentWeaponEquip.powerLevel);
         m_upgradeSpeedCost = GetUpgradeSpeedCost(playerData.currentWeaponEquip.speedLevel);
        
        m_attackPowerValue.text = playerData.currentWeaponEquip.powerValue.ToString();
        m_attackSpeedValue.text = playerData.currentWeaponEquip.speedValue.ToString();
        
        m_attackPowerLevel.text = "Level " + playerData.currentWeaponEquip.powerLevel.ToString();
        m_attackSpeedLevel.text = "Level " + playerData.currentWeaponEquip.speedLevel.ToString();


        m_attackPowerCost.text = m_upgradePowerCost / 1000 + "K";
        m_attackSpeedCost.text = m_upgradeSpeedCost / 1000 + "K";

        m_name.text = playerData.currentCharacter.ToString();

        if (currentPoints >= m_upgradePowerCost)
        {
            m_upgradePowerBtn.interactable = true;
        }
        else
        {
            m_upgradePowerBtn.interactable = false;
        }
        
        if (currentPoints >= m_upgradeSpeedCost)
        {
            m_upgradeSpeedBtn.interactable = true;
        }
        else
        {
            m_upgradeSpeedBtn.interactable = false;
        }

    }
    
    

    float GetUpgradeSpeedCost(int p_level)
    {
        
        return 53000 + (1000 * p_level);
    }
    
    float GetUpgradePowerCost(int p_level)
    {
        return 70000 + (10000 * p_level);
    }
    
    
    void OnClickBack()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    
    void UpdatePoints()
    {
        float totalPointsEarned = Profile.GetPoints();
         
        if (totalPointsEarned >= 1000000)
        {
            m_currentPointsText.text =  (totalPointsEarned / 1000000).ToString("F1") + "M";
        }
        else if ( totalPointsEarned >= 1000)
        {
            m_currentPointsText.text = (totalPointsEarned / 1000).ToString("F1") + "K";
          
        }
        else
        {
            m_currentPointsText.text = totalPointsEarned.ToString(CultureInfo.CurrentCulture);
        }
    }
    
    public void SwitchCharacter()
    {
        PlayerData playerData = PlayerDataManager.Current.GetPlayerData();
        if (playerData.currentCharacter == Characters.Shering && playerData.mingMingWeaponTier.Count > 0)
        {
            
            PlayerDataManager.Current.ChangeCharacter(Characters.MingMing);
            PlayerDataManager.Current.ChangeWeaponEquip(playerData.mingMingWeaponTier.Count);
          
        }
        else if (playerData.currentCharacter == Characters.MingMing)
        {
            PlayerDataManager.Current.ChangeCharacter(Characters.Shering);
            PlayerDataManager.Current.ChangeWeaponEquip(playerData.sheringWeaponTier.Count);
           
        }

        UpdateStats();
    }
}
