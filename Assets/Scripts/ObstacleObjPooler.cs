using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjPooler : MonoBehaviour
{
    public static ObstacleObjPooler Current;
    [SerializeField]
    private ObstaclePanel[] pooledObj;
    public int pooledAmount;


    [SerializeField]
    private List<ObstaclePanel> pooledObjsPattern1 = new List<ObstaclePanel>();
    [SerializeField]
    private List<ObstaclePanel> pooledObjsPattern2 = new List<ObstaclePanel>();
    [SerializeField]
    private List<ObstaclePanel> pooledObjsPatternRandom = new List<ObstaclePanel>();
    // Start is called before the first frame update
    void Start()
    {
        Current = this;
        for (int i = 0; i < pooledObj.Length; i++)
        {
            for (int j = 0; j < pooledAmount; j++)
            {
                ObstaclePanel obj = Instantiate<ObstaclePanel>(pooledObj[i], transform);
                obj.gameObject.SetActive(false);
                //pooledObjs.Add(obj);
                AssignPooledObj(i,obj);
            }
        }
    }

    // Update is called once per frame
   

    public GameObject GetPattern1()
    {
        for (int i = 0; i < pooledObjsPattern1.Count; i++)
        {
            if (!pooledObjsPattern1[i].gameObject.activeInHierarchy)
            {
                pooledObjsPattern1[i].SetPattern1();
                pooledObjsPattern1[i].DisableAfter(15);
                return pooledObjsPattern1[i].gameObject;
            }
        }

        return null;
    }
    

    public GameObject GetPattern2()
    {
        for (int i = 0; i < pooledObjsPattern2.Count; i++)
        {
            if (!pooledObjsPattern2[i].gameObject.activeInHierarchy)
            {
                pooledObjsPattern2[i].SetPattern2();
                pooledObjsPattern2[i].DisableAfter(15);
                return pooledObjsPattern2[i].gameObject;
            }
        }

        return null;
    }
    
    public GameObject GetPatternRandom()
    {
        List<ObstaclePanel> temp = new List<ObstaclePanel>();
        for (int i = 0; i < pooledObjsPatternRandom.Count; i++)
        {
            if (!pooledObjsPatternRandom[i].gameObject.activeInHierarchy)
            {
                //pooledObjsPatternRandom[i].SetObstacles();
                temp.Add(pooledObjsPatternRandom[i]);
            }
        }

        int randomNumber = Random.Range(0, temp.Count);

        temp[randomNumber].SetRandomPattern();
        temp[randomNumber].DisableAfter(15);
        return temp[randomNumber].gameObject;
        
        return null;
    }
    
    
    private void AssignPooledObj(int pIndex,ObstaclePanel pObj)
    {
        if (pIndex == 0)
        {
            pooledObjsPattern1.Add(pObj);
        }
        else if (pIndex == 1)
        {
            pooledObjsPattern2.Add(pObj);
        }
        else
        {
            pooledObjsPatternRandom.Add(pObj);
        }
           
    }

    public void DestroyAllActivePanel()
    {
        for (int i = 0; i < pooledObjsPatternRandom.Count; i++)
        {
            if (pooledObjsPatternRandom[i].gameObject.activeInHierarchy)
            {
                pooledObjsPatternRandom[i].DestroyEnemies();
            }
        }
        
        for (int i = 0; i < pooledObjsPattern2.Count; i++)
        {
            if (pooledObjsPattern2[i].gameObject.activeInHierarchy)
            {
                pooledObjsPattern2[i].DestroyEnemies();
            }
        }
        
        for (int i = 0; i < pooledObjsPattern1.Count; i++)
        {
            if (pooledObjsPattern1[i].gameObject.activeInHierarchy)
            {
                pooledObjsPattern1[i].DestroyEnemies();
            }
        }
    }
}
