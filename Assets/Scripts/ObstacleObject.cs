using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    private float hp;
    public TextMesh hpText;
    public MeshRenderer currentMat;


    private float startingHp;

    [SerializeField] private bool isSpecial;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag($"PlayerBullet"))
        {
            UpdateHpText(PlayerController.Current.GetBulletDamage());
        }
            if (hp <= 0)
            {
                
                SetActiveObj(false);
                GameplayController.Current.AddScore(startingHp);
                if (isSpecial)
                {
                    ObstacleObjPooler.Current.DestroyAllActivePanel();
                }
            }

            

    }


    private void SetActiveObj(bool pValue)
    {
        this.gameObject.SetActive(pValue);
    }
    
    public void DisableAfter(float pValue)
    {
        Invoke(nameof(Disable), pValue);
    }
    
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void UpdateHpText(float pValue)
    {
        hp -= pValue;
       
        if (hp >= 1000)
        {
            hpText.text = (hp / 1000).ToString("F1") + "K";
          
        }
        else
        {
            hpText.text = hp.ToString(CultureInfo.InvariantCulture);
        }

        // if (hp >= 100000000)
        // {
        //     hpText.text = (hp / 1000000).ToString("#,0M");
        // }
        //
        //  if (hp >= 10000000)
        // {
        //     hpText.text = (hp / 1000000).ToString("0.#") + "M";
        // }
        //
        //  if (hp >= 100000)
        // {
        //     hpText.text = (hp / 1000).ToString("#,0K");
        // }
        //
        // if (hp >= 10000)
        // {
        //     hpText.text = (hp / 1000).ToString("0.#") + "K";
        // }
        // else
        // {
        //     hpText.text = hp.ToString("#,0");
        //}




    }

    public void SetMaterial(Material pValue)
    {
        currentMat.material = pValue;
    }

    public void RandomizeHp()
    {
        int tempHpModifier = GameplayController.Current.GetHpModifier();
        hp = Random.Range((int)GetGreenMinHpBaseOnStats() + tempHpModifier, (int)GetRedMaxHpBaseOnStats() + tempHpModifier);
        startingHp = hp;
        if (hp >= GetRedMinHpBaseOnStats() + tempHpModifier)
        {
            SetMaterial(GameplayController.Current.listOfMaterials[3]);
        }
        else if (hp >= GetOrangeMinHpBaseOnStats() + tempHpModifier && hp <= GetOrangeMaxHpBaseOnStats() + tempHpModifier )
        {
            SetMaterial(GameplayController.Current.listOfMaterials[2]);
        }
        else if (hp >= GetYellowMinHpBaseOnStats() + tempHpModifier && hp <= GetYellowMaxHpBaseOnStats() + tempHpModifier )
        {
            SetMaterial(GameplayController.Current.listOfMaterials[1]);
        }
        else
        {
            SetMaterial(GameplayController.Current.listOfMaterials[0]);
        }
        
        UpdateHpText(0);
        SetActiveObj(true);
    }
    
    public void RandomizeHpPattern1()
    {
        int tempHpModifier = GameplayController.Current.GetHpModifier();
        hp = Random.Range((int)GetGreenMinHpBaseOnStats() + tempHpModifier, (int)GetGreenMaxHpBaseOnStats() + tempHpModifier);
        startingHp = hp;
        
        SetMaterial(GameplayController.Current.listOfMaterials[0]);
        
        UpdateHpText(0);
        SetActiveObj(true);
    }
    
    public void RandomizeHpPattern2()
    {
        int tempHpModifier = GameplayController.Current.GetHpModifier();
        hp = Random.Range((int)GetYellowMinHpBaseOnStats() + tempHpModifier, (int)GetYellowMaxHpBaseOnStats() + tempHpModifier);
        startingHp = hp;
        
            SetMaterial(GameplayController.Current.listOfMaterials[1]);
      
        
        UpdateHpText(0);
        SetActiveObj(true);
    }

    public void Killed()
    {
        SetActiveObj(false);
        GameplayController.Current.AddScore(startingHp);
    }

     float GetGreenMinHpBaseOnStats()
    {
        if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 || 
            PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
        {
            return 50 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
        }
        
        return 50 + (50 * PlayerDataManager.Current.GetPowerValueTier());
    }
     
     float GetGreenMaxHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 450 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         
         return 450 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
     
     float GetYellowMinHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 451 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         return 451 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
     
     float GetYellowMaxHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 950 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         return 950 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
     
     float GetOrangeMinHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 951 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         return 951 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
     
     float GetOrangeMaxHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 1450 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         return 1450 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
     
     float GetRedMinHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 1451 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         return 1451 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
     
     float GetRedMaxHpBaseOnStats()
     {
         if (PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 1 ||
             PlayerDataManager.Current.GetHigherTierSpeedOrPower() == 0)
         {
             return 1950 + (50 * PlayerDataManager.Current.GetSpeedValueTier());
         }
         return 1950 + (50 * PlayerDataManager.Current.GetPowerValueTier());
     }
    
   

   
    

}
