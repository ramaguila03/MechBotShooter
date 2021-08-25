using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadoutManager : MonoBehaviour
{
    public static LoadoutManager Current;
     PlayerData m_playerData;

    [SerializeField] private LoadoutItem m_prefabObj;

    [SerializeField] private Transform m_parentObj;

    private List<LoadoutItem> m_loadoutItemList;
    
    
    [SerializeField] private Button m_switchBtn;
    
    [SerializeField] private Button m_backBtn;
    
    [SerializeField] private TextMeshProUGUI m_speedValueText;
    [SerializeField] private TextMeshProUGUI m_powerValueText;
    
    [SerializeField] private TextMeshProUGUI m_charName;
    
    [SerializeField] private TextMeshProUGUI m_confirmationBodyText;
    [SerializeField] private Button m_yesBtn;
    [SerializeField] private Button m_noBtn;

    [SerializeField] private GameObject m_confirmationObject;
    
    [SerializeField] private GameObject m_propmtObject;
    [SerializeField] private Button m_okBtn;
    
    [SerializeField] private TextMeshProUGUI m_currentPointsText;

    private Action m_callback;

    private float m_currentItemCost = 0;
    
    void Start()
    {
        Current = this;
        m_playerData = new PlayerData();
        m_playerData = PlayerDataManager.Current.GetPlayerData();
        m_loadoutItemList = new List<LoadoutItem>();
        Init();
        m_switchBtn.onClick.AddListener(() => SwitchCharacter());
        m_backBtn.onClick.AddListener(() => BackBtn());
        
        m_yesBtn.onClick.AddListener(() => OnClickYes());
        m_noBtn.onClick.AddListener(() => OnClickNo());
        m_okBtn.onClick.AddListener(() => OnClickOk());
        UpdateStats();
        UpdatePoints();
    }

    public void Init()
    {
        for (int i = 1; i <= 12; i++)
        {
            LoadoutItem obj = Instantiate<LoadoutItem>(m_prefabObj, m_parentObj);
            obj.Init(m_playerData.currentCharacter, i);
            m_loadoutItemList.Add(obj);
         
        }
    }

    public void UpdateList()
    {
        m_playerData = PlayerDataManager.Current.GetPlayerData();
        for (int i = 1; i <= 12; i++)
        {
            m_loadoutItemList[i -1].Init(m_playerData.currentCharacter, i);
         
        }

        UpdateStats();
    }

    public void SwitchCharacter()
    {
        if (m_playerData.currentCharacter == Characters.Shering && m_playerData.mingMingWeaponTier.Count > 0)
        {
            
            PlayerDataManager.Current.ChangeCharacter(Characters.MingMing);
            PlayerDataManager.Current.ChangeWeaponEquip(m_playerData.mingMingWeaponTier.Count);
           
        }
        else if (m_playerData.currentCharacter == Characters.MingMing)
        {
            PlayerDataManager.Current.ChangeCharacter(Characters.Shering);
            PlayerDataManager.Current.ChangeWeaponEquip(m_playerData.sheringWeaponTier.Count);
          
        }

        UpdateList();
    }

    void UpdateStats()
    {
        m_powerValueText.text = m_playerData.currentWeaponEquip.powerValue.ToString();
        m_speedValueText.text = m_playerData.currentWeaponEquip.speedValue.ToString();
        m_charName.text = m_playerData.currentCharacter.ToString();
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
    void BackBtn()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void OpenConfirmation(string p_itemName, float p_cost, Action p_callback)
    {
        m_currentItemCost = p_cost;
        m_confirmationObject.SetActive(true);
        m_confirmationBodyText.text = "Would you like to purchase " + p_itemName;
        m_callback = p_callback;
    }

    void OnClickYes()
    {
        float currentPoints = Profile.GetPoints();
        if (currentPoints >= m_currentItemCost)
        {
            if (m_callback != null)
            {
                Profile.SubtractPoints(m_currentItemCost);
                UpdatePoints();
                m_callback.Invoke();
            }
        }
        else
        {
            m_propmtObject.SetActive(true);
        }
      
        m_confirmationObject.SetActive(false);
    }

    void OnClickNo()
    {
        m_confirmationObject.SetActive(false);
    }

    void OnClickOk()
    {
        m_propmtObject.SetActive(false);
    }







}
