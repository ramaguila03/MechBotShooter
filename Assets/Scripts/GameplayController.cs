using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Current;
    
    public Material[] listOfMaterials;
    // Start is called before the first frame update

    private int waveCount = 0;
    
    public int hpIncrease = 0;

    private GameObject pattern;

    [SerializeField] 
    private TextMeshProUGUI scoreText;
    
    [SerializeField] 
    private TextMeshProUGUI highscoreText;
    
    [SerializeField] 
    private TextMeshProUGUI totalPointsEarnedText;
    
    [SerializeField] 
    private TextMeshProUGUI currentPointsEarnedText;

    private float score;
    
    [SerializeField] 
    
    public TextMeshProUGUI waveCountText;
    [SerializeField] 
    
    public TextMeshProUGUI hpIncreaseText;

    [SerializeField] private GameObject gameOver;

    [SerializeField] private Button retryBtn;
    
    [SerializeField] private Button mainMenuBtn;
    
    [SerializeField] private Button m_upgradeSpeedBtn;
    [SerializeField] private Button m_upgradePowerBtn;
    
    [SerializeField] private TextMeshProUGUI m_attackPowerValue;
    [SerializeField] private TextMeshProUGUI m_attackSpeedValue;
    
    [SerializeField] private TextMeshProUGUI m_attackPowerLevel;
    [SerializeField] private TextMeshProUGUI m_attackSpeedLevel;
    
    [SerializeField] private TextMeshProUGUI m_attackPowerCost;
    [SerializeField] private TextMeshProUGUI m_attackSpeedCost;
    
    [SerializeField] private Button m_yesBtn;
    [SerializeField] private Button m_noBtn;
    [SerializeField] private Button m_loadoutBtn;
    [SerializeField] private Button m_pauseBtn;
    
    [SerializeField] private GameObject m_confirmationObject;
    
    float m_upgradePowerCost; 
    float m_upgradeSpeedCost;

    void Awake()
    {
        Current = this;
    }

    void Start()
    {
        int tempPattern = GetPattern();
        InvokeRepeating(("SpawnObstacles"), 0, 3);
        InvokeRepeating(("SpawnPowerUp"), 9, 9);
        retryBtn.onClick.AddListener(() => Retry());
        mainMenuBtn.onClick.AddListener(() => MainMenu());
        m_yesBtn.onClick.AddListener(() => MainMenu());
        m_noBtn.onClick.AddListener(() => OnClickNo());
        m_upgradeSpeedBtn.onClick.AddListener(() => OnClickUpgradeSpeed());
        m_upgradePowerBtn.onClick.AddListener(() => OnClickUpgradePower());
        m_loadoutBtn.onClick.AddListener(() => OnClickLoadoutBtn());
        m_pauseBtn.onClick.AddListener(() => OnClickPause());
        ShowHighScore();
    }
    
    void SpawnObstacles()
    {
        waveCount++;
        waveCountText.text = "Wave : " + waveCount;
        int tempPattern = GetPattern();
        
        if (tempPattern == 1)
        {
            pattern = ObstacleObjPooler.Current.GetPattern1();
        }
        else if (tempPattern == 2)
        {
            pattern = ObstacleObjPooler.Current.GetPattern2();
        }
        else
        {
            pattern = ObstacleObjPooler.Current.GetPatternRandom();
        }

        pattern.transform.position = transform.position;
        pattern.SetActive(true);

        SetHpModifier();
        hpIncreaseText.text = "Hp Increase : " + GetHpModifier();
        
    }

    void SpawnPowerUp()
    {

        int random = Random.Range(0, 2);

        if (random == 0)
        {
            GameObject powerUp = PowerUpObjPooler.Current.GetRandomPowerUp();
            powerUp.transform.position = transform.position + new Vector3(Random.Range(-4, 4), -5, 13);
            powerUp.SetActive(true);
        }
        else
        {
            GameObject powerUp = PowerUpObjPooler.Current.GetSpecialEnemy();
            powerUp.transform.position = transform.position + new Vector3(Random.Range(-4, 4), -5, 13);
            powerUp.SetActive(true);
        }



    }



    public int GetHpModifier()
    {
        return hpIncrease;
    }

    void SetHpModifier()
    {
        if (waveCount >= 6)
        {
            int tempNumber = 0;
            tempNumber = (waveCount + 4) - 6;
            
            if (tempNumber % 4 == 0)
            {
                hpIncrease += 20;
            }
        }
    }

    public int GetPattern()
    {
        int tempPattern = 3;
        int tempNumber = 0;
        
        if (waveCount == 1)
        {
            tempPattern = 1;
        }
        else if (waveCount == 2 || waveCount == 3)
        {
            tempPattern = 2;
        }
        else if (waveCount == 4 || waveCount == 5)
        {
            tempPattern = 3;
        }
        else if (waveCount >= 6)
        {
            for(int i = 6; i < 9; i++)
            {
                tempNumber = (waveCount + 4) - i;
                if (tempNumber % 4 == 0)
                {
                    if (i == 6 || i == 7)
                    {
                        tempPattern = 2;
                    }
                    else
                    {
                        tempPattern = 3;
                    }

                    return tempPattern;
                }
            }
           
        }


        return tempPattern;
    }

    public void AddScore(float pValue)
    {
        score += pValue;
        if (score >= 1000)
        {
            scoreText.text = (score / 1000).ToString("F1") + "K";
          
        }
        else
        {
            scoreText.text = score.ToString(CultureInfo.CurrentCulture);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        if (score > Profile.GetHighestScore())
        {
            Profile.SetHighestScore(score);
            ShowHighScore();
        }

        ShowCurrentPointsEarned();
        ShowTotalPointsEarned();
        UpdateStats();
     
        
    }

     void Retry()
     {
         Time.timeScale = 1;
         SceneManager.LoadScene(sceneBuildIndex: 1);
     }
     
     void MainMenu()
     {
         Time.timeScale = 1;
         SceneManager.LoadScene(sceneBuildIndex: 0);
     }

     void ShowHighScore()
     {
         float highscore = Profile.GetHighestScore();
         if (highscore >= 1000000)
         {
             highscoreText.text =  (highscore / 1000000).ToString("F1") + "M";
         }
         else if ( highscore >= 1000)
         {
             highscoreText.text = (highscore / 1000).ToString("F1") + "K";
          
         }
         else
         {
             highscoreText.text = highscore.ToString(CultureInfo.CurrentCulture);
         }
     }
     
     void ShowTotalPointsEarned()
     {
         float totalPointsEarned = Profile.GetPoints();
         
         if (totalPointsEarned >= 1000000)
         {
             totalPointsEarnedText.text =  (totalPointsEarned / 1000000).ToString("F1") + "M";
         }
         else if ( totalPointsEarned >= 1000)
         {
             totalPointsEarnedText.text = (totalPointsEarned / 1000).ToString("F1") + "K";
          
         }
         else
         {
             totalPointsEarnedText.text = totalPointsEarned.ToString(CultureInfo.CurrentCulture);
         }
     }

     void ShowCurrentPointsEarned()
     {
         currentPointsEarnedText.text = scoreText.text;
         Profile.AddPoints(score);
     }
     
     float GetUpgradeSpeedCost(int p_level)
     {
        
         return 53000 + (1000 * p_level);
     }
    
     float GetUpgradePowerCost(int p_level)
     {
         return 70000 + (10000 * p_level);
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
     
     void OnClickUpgradeSpeed()
     {
         Profile.SubtractPoints(m_upgradeSpeedCost);
         PlayerDataManager.Current.UpgradeSpeed();
         UpdateStats();
         ShowTotalPointsEarned();
     }
    
     void OnClickUpgradePower()
     {
         Profile.SubtractPoints(m_upgradePowerCost);
         PlayerDataManager.Current.UpgradePower();
         UpdateStats();
         ShowTotalPointsEarned();
     }

     void OnClickPause()
     {
         m_confirmationObject.SetActive(true);
         Time.timeScale = 0;
     }

     void OnClickNo()
     {
         m_confirmationObject.SetActive(false);
         Time.timeScale = 1;
     }

     void OnClickLoadoutBtn()
     {
         Time.timeScale = 1;
         SceneManager.LoadScene(sceneBuildIndex: 2);
     }
     
    
     
     

}
