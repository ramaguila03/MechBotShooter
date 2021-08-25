using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public float speed;

    public Rigidbody rb;
    public Vector3 movement;
    // Start is called before the first frame update

    private void OnEnable()
    {

        Invoke(nameof(Disable), 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Side") )
        {
           
            Disable();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        movement = new Vector3(0, 0, 1);
        rb.MovePosition(transform.position + movement * (speed * Time.fixedDeltaTime));
    }

    // Update is called once per frame

    private void Disable()
    {
      
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
