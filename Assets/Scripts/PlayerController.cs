using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;
   public float speed = 10f;
   public Vector3 movement;
 
    public Rigidbody rb;
    
    public  float refireRate = 2f;
    
    public float fireTimer = 0;

    public float bulletDamage = 0;

    [SerializeField]
    private bool isSpreadActive = false;
    
    [SerializeField]
    private bool isPowerBoostActive = false;
    
    [SerializeField]
    private bool isSpeedBoostActive = false;
    
    [SerializeField]
    private bool isUltimateBoostActive = false;

    private Touch touch;

    private float speedModifier;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            GameplayController.Current.GameOver();
        }
        
        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            StartCoroutine(ActivateSpeedBoost());
        }
        
        if (other.gameObject.CompareTag("PowerBoost"))
        {
            StartCoroutine(ActivatePowerBoost());
        }
        
        if (other.gameObject.CompareTag("Spread"))
        {
            StartCoroutine(ActivateSpread());
        }
        
        if (other.gameObject.CompareTag("UltimateBoost"))
        {
            StartCoroutine(ActivateUltimateBoost());
        }

    }

    void Start()
    {
        Current = this;
        speedModifier = 0.006f;
        refireRate = PlayerDataManager.Current.GetActualRateOfFire();
        bulletDamage = PlayerDataManager.Current.GetPlayerData().currentWeaponEquip.powerValue;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {   
        rb.velocity = Vector3.zero;
        
#if UNITY_EDITOR || UNITY_WEBGL
        rb.MovePosition(transform.position + movement * (speed * Time.fixedDeltaTime));
        
       
#else

       if (transform.position.x >= 4.44f)
        {
            rb.MovePosition(new Vector3(4.4f,transform.position.y, transform.position.z));
           // transform.position = new Vector3(4.5f,transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -4.66f)
        {
           rb.MovePosition(new Vector3(-4.6f,transform.position.y, transform.position.z));
            //transform.position = new Vector3(-4.5f,transform.position.y, transform.position.z);
        }
        else
        {
            rb.MovePosition(transform.position + movement * speedModifier);
        }
     

#endif
        
        fireTimer += Time.deltaTime;

        if (isSpeedBoostActive || isUltimateBoostActive)
        {
            float tempRefireRate = refireRate / 2;
            if (fireTimer >= tempRefireRate)
            {
                fireTimer = 0;
                Fire();
            }
        }
        else
        {
            if (fireTimer >= refireRate)
            {
                fireTimer = 0;
                Fire();
            }
        }
    }

    private void Update()
    {
       
        #if UNITY_EDITOR || UNITY_WEBGL
        movement = new Vector3(Input.GetAxis("Horizontal"), 0,0);
       
     
    #else
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {

                float x = touch.deltaPosition.x;

                   

                    movement = new Vector3(x, 0, 0);

                
            }
else
{
 movement = new Vector3(0, 0, 0);
}
        }

     

#endif


    }

    void Fire()
    {
        GameObject obj = BulletObjPooler.Current.GetPooledObject();
        obj.transform.position = transform.position + new Vector3(0,0.5f,2);
        obj.SetActive(true);

        if (isSpreadActive)
        {
            GameObject obj2 = BulletObjPooler.Current.GetPooledObject();
            obj2.transform.position = transform.position + new Vector3(-1.5f, 0.5f, 2);
            obj2.SetActive(true);

            GameObject obj3 = BulletObjPooler.Current.GetPooledObject();
            obj3.transform.position = transform.position + new Vector3(1.5f, 0.5f, 2);
            obj3.SetActive(true);
        }
    }
    
  

    public float GetBulletDamage()
    {
        if (isPowerBoostActive || isUltimateBoostActive)
        {
            return bulletDamage * 2;
        }
        return bulletDamage;
    }

    IEnumerator ActivateSpread()
    {
        isSpreadActive = true;
        yield return new WaitForSeconds(5f);
        isSpreadActive = false;
    }
    
    IEnumerator ActivatePowerBoost()
    {
        isPowerBoostActive = true;
        yield return new WaitForSeconds(5f);
        isPowerBoostActive = false;
    }
    
    IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        yield return new WaitForSeconds(5f);
        isSpeedBoostActive = false;
    }
    
    IEnumerator ActivateUltimateBoost()
    {
        isUltimateBoostActive = true;
        yield return new WaitForSeconds(5f);
        isUltimateBoostActive = false;
    }
    
   
}
