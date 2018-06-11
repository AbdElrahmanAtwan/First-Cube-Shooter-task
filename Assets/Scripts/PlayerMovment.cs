using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    [Header("Movement")]
    public float movingSpeed = 5.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.forward * movingSpeed,ForceMode.Impulse);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-Vector3.forward * movingSpeed, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.D))
           rb.AddForce(Vector3.right * movingSpeed,ForceMode.Impulse);

        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-Vector3.right * movingSpeed, ForceMode.Impulse);
    }
}   
