
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class WeaponClass
{
    public int tier;
    public int speedLevel;
    public int powerLevel;
    public float speedValue;
    public float powerValue;

    public WeaponClass(int p_tier, int p_speedLevel, int p_powerLevel, float p_speedValue, float p_powerValue)
    {
        tier = p_tier;
        speedLevel = p_speedLevel;
        powerLevel = p_powerLevel;
        speedValue = p_speedValue;
        powerValue = p_powerValue;
    }
}

[Serializable]
public enum Characters
{
    Shering,
    MingMing
}

[Serializable]
public class PlayerData
{
    public Characters currentCharacter;
    public WeaponClass currentWeaponEquip;
    
    public List<WeaponClass> sheringWeaponTier;
    public List<WeaponClass> mingMingWeaponTier;
    
}

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Current;
    public PlayerData m_playerdata = new PlayerData();
    
    
   void Awake()
   {
       DontDestroyOnLoad (this);
         
       if (Current == null) {
           Current = this;
           LoadPlayerData();
       } else {
           Destroy(this.gameObject);
           
       }

     

   }

   void InitilizeList()
   {
       m_playerdata.sheringWeaponTier = new List<WeaponClass>();
       m_playerdata.mingMingWeaponTier = new List<WeaponClass>();
   }


   void Update()
   {
       if(Input.GetKeyDown(KeyCode.S))
       {
           SavePlayerData();
           Debug.Log("save");
       }
       
       if(Input.GetKeyDown(KeyCode.L))
       {
          LoadPlayerData();
          Debug.Log("load");
       }
       
       if(Input.GetKeyDown(KeyCode.K))
       {
           Profile.AddPoints(100000);
       }
       
       if(Input.GetKeyDown(KeyCode.L))
       {
           Profile.AddPoints(14000);
       }
   }
   
   
   
   public void AddWeaponTier(Characters p_character, WeaponClass p_weaponUpgrade)
   {
       
       if (p_character == Characters.Shering)
       {
            m_playerdata.sheringWeaponTier.Add(p_weaponUpgrade);
       }
       else
       {
           m_playerdata.mingMingWeaponTier.Add(p_weaponUpgrade);
       }
       
   }

   public void SavePlayerData()
   {
       PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(m_playerdata));
       PlayerPrefs.Save();
   }

   void LoadPlayerData()
   {
       string playerDataSave = PlayerPrefs.GetString("PlayerData");
       if (playerDataSave == "")
       {
           Debug.Log("new");
           InitilizeList();
           AddWeaponTier(Characters.Shering, new WeaponClass(1,1,1,8.5f,55f));
           m_playerdata.currentWeaponEquip = new WeaponClass(1, 1, 1, 8.5f, 55f);
           return;
       }
       m_playerdata = JsonUtility.FromJson<PlayerData>(playerDataSave);
       Debug.Log("load from save");

   }

   public void UpdateWeaponTierList()
   {
       if (m_playerdata.currentCharacter == Characters.Shering)
       {
           for (int i = 0; i < m_playerdata.sheringWeaponTier.Count; i++)
           {
               if (m_playerdata.currentWeaponEquip.tier == m_playerdata.sheringWeaponTier[i].tier)
               {
                   m_playerdata.sheringWeaponTier[i] = m_playerdata.currentWeaponEquip;
               }
           }
       }
   }

   public PlayerData GetPlayerData()
   {
       return m_playerdata;
   }

   public float GetWeaponPowerBaseStat(Characters p_character, int p_tier)
   {
       if (p_character == Characters.Shering)
       {
           return 6 + (p_tier - 1) * 18;
       }
       
       else if (p_character == Characters.MingMing)
       {
           return 204 + (p_tier - 1) * 18;
       }

       return 6;
   }
   
   public float GetWeaponSpeedBaseStat(Characters p_character, int p_tier)
   {
       if (p_character == Characters.Shering)
       {
           switch (p_tier)
           {
               case 1:
               case 2:
               case 3:
               case 4:
               case 5: 
                   return 2.0f;
               case 6:
                   case 7:
                   case 8:
                   return 2.5f;
               case 9:
                   case 10:
                   return 3;
               case 11:
                   return 3.5f;
                   
           }
       }
       
       else if (p_character == Characters.MingMing)
       {
           switch (p_tier)
           {
               case 1:
               case 2:
               case 3:
               case 4:
               case 5: 
                   return 2.5f;
               case 6:
               case 7:
               case 8:
                   return 3;
               case 9:
               case 10:
                   return 3.5f;
               case 11:
                   return 4;
                   
           }
       }

       return 2.0f;
   }

   public float GetCharacterSpeedbaseStat(Characters p_character)
   {
       return 6.5f;
   }
   
   public float GetCharacterPowerbaseStat(Characters p_character)
   {
       return 49f;
   }

   public void ChangeWeaponEquip(int p_tier)
   {
       if (m_playerdata.currentCharacter == Characters.Shering)
       {
           m_playerdata.currentWeaponEquip = m_playerdata.sheringWeaponTier[p_tier - 1];
       }
       
       if (m_playerdata.currentCharacter == Characters.MingMing)
       {
           m_playerdata.currentWeaponEquip = m_playerdata.mingMingWeaponTier[p_tier - 1];
       }
       
       SavePlayerData();
   }

   public void ChangeCharacter(Characters p_character)
   {
       m_playerdata.currentCharacter = p_character;
   }

   public float GetActualRateOfFire()
   {
       float shootingSpeed = m_playerdata.currentWeaponEquip.speedValue;
       if (shootingSpeed == 8.5f)
       {
           return 0.15f;
       }
       float speedTierDifference =  (shootingSpeed - 8.5f) / 0.5f;
       
       return  0.15f - (0.01f * speedTierDifference);
       
       
   }

   public float GetSpeedValueTier()
   {
       float shootingSpeed = m_playerdata.currentWeaponEquip.speedValue;
       float speedTierDifference = (shootingSpeed - 8f) / 0.5f;
       return speedTierDifference;
   }
   
   public float GetPowerValueTier()
   {

       float shootingPower = m_playerdata.currentWeaponEquip.powerValue;
       float powerTierDifference = (shootingPower - 48) / 7;
       return powerTierDifference;
   }

   public int GetHigherTierSpeedOrPower()
   {
       float speedTier = GetSpeedValueTier();
       float powerTier = GetPowerValueTier();

       if (speedTier > powerTier)
       {
           return 1;
       }
       else if (powerTier > speedTier)
       {
           return 2;
       }

       return 0;
   }
   
   public void UpgradeSpeed()
   {
      
       m_playerdata.currentWeaponEquip.speedLevel++;
       m_playerdata.currentWeaponEquip.speedValue += 0.5f;
       UpdateWeaponTierList();
       SavePlayerData();

   }

   public void UpgradePower()
   {
       m_playerdata.currentWeaponEquip.powerLevel++;
       m_playerdata.currentWeaponEquip.powerValue += 7f;
       UpdateWeaponTierList();
       SavePlayerData();
   }
   
   

   
}


