using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObjPooler : MonoBehaviour
{
    public static PowerUpObjPooler Current;
    public PowerUpObj[] pooledPowerObj;
    public int pooledAmount;

 

    public List<PowerUpObj> pooledPowerUpObjs;
    public List<ObstacleObject> pooledSpecialEnemies;

    public ObstacleObject pooledSpecialEnemy;
    void Start()
    {
        Current = this;
        for (int i = 0; i < pooledPowerObj.Length; i++)
        {
            for (int j = 0; j < pooledAmount; j++)
            {
                PowerUpObj obj = Instantiate<PowerUpObj>(pooledPowerObj[i], transform);
                obj.gameObject.SetActive(false);
                pooledPowerUpObjs.Add(obj);
               
            }
        }
        
        for (int i = 0; i < pooledAmount; i++)
        {
            ObstacleObject obj = Instantiate<ObstacleObject>(pooledSpecialEnemy, transform);
            obj.gameObject.SetActive(false);
            pooledSpecialEnemies.Add(obj);
        }
    }
    
    public GameObject GetRandomPowerUp()
    {
        List<PowerUpObj> temp = new List<PowerUpObj>();
        for (int i = 0; i < pooledPowerUpObjs.Count; i++)
        {
            if (!pooledPowerUpObjs[i].gameObject.activeInHierarchy)
            {
                //pooledObjsPatternRandom[i].SetObstacles();
                temp.Add(pooledPowerUpObjs[i]);
            }
        }
        
        

        int randomNumber = Random.Range(0, temp.Count);
        
        temp[randomNumber].DisableAfter(10);
        return temp[randomNumber].gameObject;
        
        return null;
    }
    
    public GameObject GetSpecialEnemy()
    {
        for (int i = 0; i < pooledSpecialEnemies.Count; i++)
        {
            if (!pooledSpecialEnemies[i].gameObject.activeInHierarchy)
            {
                pooledSpecialEnemies[i].RandomizeHp();
                pooledSpecialEnemies[i].DisableAfter(10);
                return pooledSpecialEnemies[i].gameObject;
            }
        }

        return null;
    }
}
