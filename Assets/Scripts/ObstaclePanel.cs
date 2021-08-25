using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePanel : MonoBehaviour
{
    public ObstacleObject[] obstacleObjs;
    public float speed;

    public Rigidbody rb;
    public Vector3 movement;
    // Start is called before the first frame update

  

    public void DisableAfter(float pValue)
    {
        Invoke(nameof(Disable), pValue);
    }

    
    void FixedUpdate()
    {
        movement = new Vector3(0, 0, -1);
        rb.MovePosition(transform.position + movement * (speed * Time.fixedDeltaTime));
    }
    
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetRandomPattern()
    {
        for (int i = 0; i < obstacleObjs.Length; i++)
        {
            obstacleObjs[i].RandomizeHp();
        }
    }
    
    public void SetPattern1()
    {
        for (int i = 0; i < obstacleObjs.Length; i++)
        {
            obstacleObjs[i].RandomizeHpPattern1();
        }
    }
    
    public void SetPattern2()
    {
        int random = Random.Range(0, obstacleObjs.Length);
        
        for (int i = 0; i < obstacleObjs.Length; i++)
        {
            if (random == i)
            {
                obstacleObjs[i].RandomizeHpPattern1();
            }
            else
            {
                obstacleObjs[i].RandomizeHpPattern2();
            }
           
        }
    }

    public void DestroyEnemies()
    {
        for (int i = 0; i < obstacleObjs.Length; i++)
        {
            obstacleObjs[i].Killed();
        }
    }
    
    
    
}
