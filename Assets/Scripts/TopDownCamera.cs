using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform m_target;
    public float m_height = 10f;
    public float m_distance = 20f;
    public float m_angle = 45f;


   

    // Update is called once per frame
    void Start()
    {
        HandleCamera();
    }

    void Update()
    {
        HandleCamera();
    }

    void HandleCamera()
    {
        if(!m_target)
        {
            return;
        }
        Vector3 worldPosition = (Vector3.forward * -m_distance) + (Vector3.up * m_height);

    }
    
}
