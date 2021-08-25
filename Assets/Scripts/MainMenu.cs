using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_startBtn;
    
    [SerializeField] private Button m_upgradeBtn;
    
    [SerializeField] private Button m_loadoutBtn;
    
    [SerializeField] private Button m_switchCharacter;
    
    [SerializeField] private TextMeshProUGUI m_speedValueText;
    [SerializeField] private TextMeshProUGUI m_powerValueText;
    
    [SerializeField] private TextMeshProUGUI m_charName;
   

    [SerializeField] private Button m_cheatAddScore;
    
    private PlayerData m_playerData;

    void Start()
    {
        UpdateLocalPlayerData();
        m_startBtn.onClick.AddListener(StartGame);
        m_loadoutBtn.onClick.AddListener(OnClickLoadoutBtn);
        m_switchCharacter.onClick.AddListener(OnClickSwitchCharacter);
        m_upgradeBtn.onClick.AddListener(OnClickUpgrade);
        m_cheatAddScore.onClick.AddListener(AddScore);
        UpdateStats();
    }
    void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
     
    void Exit()
    {
       Application.Quit();
       
    }

    void UpdateLocalPlayerData()
    {
        m_playerData = PlayerDataManager.Current.GetPlayerData();
    }
    
    void UpdateStats()
    {
        m_powerValueText.text = m_playerData.currentWeaponEquip.powerValue.ToString();
        m_speedValueText.text = m_playerData.currentWeaponEquip.speedValue.ToString();
        m_charName.text = m_playerData.currentCharacter.ToString();
    }

    void OnClickLoadoutBtn()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }
    
    void OnClickUpgrade()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }
    
    public void OnClickSwitchCharacter()
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

        UpdateLocalPlayerData();
        UpdateStats();

    }

    void AddScore()
    {
        Profile.AddPoints(100000);
    }
}
