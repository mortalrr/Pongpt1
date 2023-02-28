using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaddleController : MonoBehaviour
{
    private Vector2 movement;
    

    public float unitsPerSecond = 3f;
    public float speed = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        Vector3 force = Vector3.up * horizontalValue * unitsPerSecond * Time.deltaTime;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Force);
        
        float verticalValue = Input.GetAxis("Vertical");
        Vector3 forcee = Vector3.up * verticalValue * speed * Time.deltaTime;

        Rigidbody rbRigidbody = GetComponent<Rigidbody>();
        rbRigidbody.AddForce(forcee, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
