using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObj : MonoBehaviour
{
    public float speed;

    public Rigidbody rb;
    private Vector3 movement;
    
    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        movement = new Vector3(0, 0, -1);
        rb.MovePosition(transform.position + movement * (speed * Time.fixedDeltaTime));
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Disable();
        }

    }
    public void DisableAfter(float pValue)
    {
        Invoke(nameof(Disable), pValue);
    }
    
    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
