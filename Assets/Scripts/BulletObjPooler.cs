using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjPooler : MonoBehaviour
{
    public static BulletObjPooler Current;
    public GameObject pooledObj;
    public int pooledAmount;
    public bool willGrow;

 

    public List<GameObject> pooledObjs;
    // Start is called before the first frame update
    void Start()
    {
        Current = this;
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObj, transform);
            obj.SetActive(false);
            pooledObjs.Add(obj);
        }
    }

    // Update is called once per frame
   

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjs.Count; i++)
        {
            if (!pooledObjs[i].activeInHierarchy)
            {
                return pooledObjs[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObj, transform);
            pooledObjs.Add(obj);
            return obj;
        }
        
        return null;
    }
}
