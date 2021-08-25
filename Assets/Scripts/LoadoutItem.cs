using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadoutItem : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private TextMeshProUGUI m_name;
    [SerializeField] private TextMeshProUGUI m_attackPower;
    [SerializeField] private TextMeshProUGUI m_attackSpeed;
    [SerializeField] private TextMeshProUGUI m_costText;
    [SerializeField] private GameObject m_lockImage;
    [SerializeField] private GameObject m_equipImage;
    [SerializeField] private GameObject m_costImage;
    [SerializeField] private Image m_itemImage;
    [SerializeField] private TextMeshProUGUI m_newCharacterText;


    private int m_tier;
    private Characters m_character;
    
    // Start is called before the first frame update

    public void Init(Characters p_character, int p_tier)
    {
        m_tier = p_tier;
        m_character = p_character;
        if (p_tier != 12)
        {
          
            m_name.text = p_character.ToString() + " No. " + p_tier;
            m_attackPower.text = "Attack Power: " + PlayerDataManager.Current.GetWeaponPowerBaseStat(p_character, p_tier);
            m_attackSpeed.text = "Attack Speed: " + PlayerDataManager.Current.GetWeaponSpeedBaseStat(p_character, p_tier); 
            if (p_character == Characters.Shering)
        {
            if (p_tier - PlayerDataManager.Current.GetPlayerData().sheringWeaponTier.Count == 1)
            {
                m_costImage.SetActive(true);
                SetCostText(p_character, p_tier);
                SetLockImage(false);
                m_equipImage.SetActive(false);
                m_button.interactable = true;
                m_button.onClick.RemoveAllListeners();
                m_button.onClick.AddListener(() => PurchaseConfirmation());

            }
            
            else if(p_tier == PlayerDataManager.Current.GetPlayerData().currentWeaponEquip.tier)
            {
                m_equipImage.SetActive(true);
                SetLockImage(false);
                m_costImage.SetActive(false);
                m_button.interactable = true;
                m_button.onClick.RemoveAllListeners();
            }
          
            else if(p_tier > PlayerDataManager.Current.GetPlayerData().sheringWeaponTier.Count)
            {
                m_costImage.SetActive(true);
                SetCostText(p_character, p_tier);
                SetLockImage(true);
                m_equipImage.SetActive(false);
                m_button.interactable = false;
                
            }
            else
            {
                SetLockImage(false);
                m_equipImage.SetActive(false);
                m_costImage.SetActive(false);

                m_button.interactable = true;
                m_button.onClick.RemoveAllListeners();
                m_button.onClick.AddListener(() => Equip());
              
            }
        }
        else if (p_character == Characters.MingMing)
        {
            if (p_tier - PlayerDataManager.Current.GetPlayerData().mingMingWeaponTier.Count == 1)
            {
                m_costImage.SetActive(true);
                SetCostText(p_character, p_tier);
                SetLockImage(false);
                m_equipImage.SetActive(false);
                m_button.interactable = true;
                m_button.onClick.RemoveAllListeners();
                m_button.onClick.AddListener(() => PurchaseConfirmation());

            }
            
            else if(p_tier == PlayerDataManager.Current.GetPlayerData().currentWeaponEquip.tier)
            {
                m_equipImage.SetActive(true);
                SetLockImage(false);
                m_costImage.SetActive(false);
                m_button.interactable = true;
                m_button.onClick.RemoveAllListeners();
            }
          
            else if(p_tier > PlayerDataManager.Current.GetPlayerData().mingMingWeaponTier.Count)
            {
                m_costImage.SetActive(true);
                SetCostText(p_character, p_tier);
                SetLockImage(true);
                m_equipImage.SetActive(false);
                m_button.interactable = false;
                
            }
            else
            {
                SetLockImage(false);
                m_equipImage.SetActive(false);
                m_costImage.SetActive(false);
                m_button.onClick.RemoveAllListeners();
                m_button.onClick.AddListener(() => Equip());
              
            }
        }
        }
        else
        {
            m_name.gameObject.SetActive(false);
            m_attackPower.gameObject.SetActive(false);
            m_attackSpeed.gameObject.SetActive(false);
         
            m_lockImage.gameObject.SetActive(false);
            m_equipImage.gameObject.SetActive(false);
            m_newCharacterText.gameObject.SetActive(true);
               m_costImage.gameObject.SetActive(false);
              
         
            if (p_character == Characters.Shering)
            {
                if (PlayerDataManager.Current.GetPlayerData().sheringWeaponTier.Count == 11 && PlayerDataManager.Current.GetPlayerData().mingMingWeaponTier.Count == 0 )
                {
                    m_costImage.gameObject.SetActive(true);
                    m_button.interactable = true;
                    m_newCharacterText.text = "Unlock New Character";
                    SetCostText(Characters.MingMing, 1);
                    m_button.onClick.RemoveAllListeners();
                    m_button.onClick.AddListener(() => PurchaseConfirmation());
                    
                    
                }
                else if (PlayerDataManager.Current.GetPlayerData().sheringWeaponTier.Count == 11 && PlayerDataManager.Current.GetPlayerData().mingMingWeaponTier.Count > 0)
                {
                    m_newCharacterText.text = "Next Character Already Unlocked";
                    m_button.interactable = false;
                    m_costImage.gameObject.SetActive(false);
                }
                else
                {
                    m_button.interactable = false;
                    m_newCharacterText.text = "Unlock New Character";
                    m_costImage.gameObject.SetActive(true);
                    SetCostText(Characters.MingMing, 1);
                    
                }
            }
            else
            {
                m_button.interactable = false;
                m_newCharacterText.text = "No New Character";
            }

        }


      
    }

    void SetCostText(Characters p_character, int p_tier)
    {
        float cost = GetCost(p_character, p_tier);
    
         if (cost >= 1000000)
        {
            m_costText.text = cost / 1000000 + "M";
        }
        else if (cost >= 1000)
        {
            m_costText.text = cost / 1000 + "K";
        }
    }

    public void SetLockImage(bool p_value)
    {
        m_lockImage.SetActive(p_value);
    }

    float GetCost(Characters p_character, int p_tier)
    {
        if (p_character == Characters.Shering)
        {
            return 50000 * p_tier;
        }
        
        else if (p_character == Characters.MingMing)
        {
            return 550000 + (50000 * p_tier);
        }

        return 50000;
    }


    public int GetTier()
    {
        return m_tier;
    }

    public void Equip()
    {
        Debug.Log("equip");
        PlayerDataManager.Current.ChangeWeaponEquip(m_tier);
        LoadoutManager.Current.UpdateList();
    }

    void PurchaseConfirmation()
    {
        string name = "";
        if (m_tier != 12)
        {
             name = m_character.ToString() + " No. " + m_tier;
        }
        else
        {
            name = Characters.MingMing.ToString();
        }

        float cost = GetCost(m_character, m_tier);
        string costText = "";
            
        if (cost >= 1000000)
        {
            costText =  " with " + cost / 1000000 + "M";
        }
        else if (cost >= 1000)
        {
            costText = " with " + cost / 1000 + "K";
        }

        if (m_tier != 12)
        {
            LoadoutManager.Current.OpenConfirmation(name + costText, cost, Buy);
        }
        else
        {
            LoadoutManager.Current.OpenConfirmation(name + costText, cost, UnlockNextCharacter);
        }
    }

    public void Buy()
    {
        PlayerData playerData = PlayerDataManager.Current.GetPlayerData();
        PlayerDataManager.Current.AddWeaponTier(m_character, new WeaponClass(m_tier, 1, 1,
            PlayerDataManager.Current.GetWeaponSpeedBaseStat(m_character, m_tier) + PlayerDataManager.Current.GetCharacterSpeedbaseStat(m_character),
            PlayerDataManager.Current.GetWeaponPowerBaseStat(m_character, m_tier) + PlayerDataManager.Current.GetCharacterPowerbaseStat(m_character)));
        Equip();
        //PlayerDataManager.Current.SavePlayerData();
        //LoadoutManager.Current.UpdateList();

    }

    public void UnlockNextCharacter()
    {
        PlayerData playerData = PlayerDataManager.Current.GetPlayerData();
        PlayerDataManager.Current.AddWeaponTier(Characters.MingMing, new WeaponClass(1, 1, 1,
            PlayerDataManager.Current.GetWeaponSpeedBaseStat(Characters.MingMing, 1) +
            PlayerDataManager.Current.GetCharacterSpeedbaseStat(Characters.MingMing),
            PlayerDataManager.Current.GetWeaponPowerBaseStat(Characters.MingMing, 1) +
            PlayerDataManager.Current.GetCharacterPowerbaseStat(Characters.MingMing)));
        PlayerDataManager.Current.ChangeCharacter(Characters.MingMing);
        PlayerDataManager.Current.ChangeWeaponEquip(1);
        LoadoutManager.Current.UpdateList();

    }


}
