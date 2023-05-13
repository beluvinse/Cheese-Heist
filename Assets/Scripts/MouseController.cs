using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : SteeringAgent
{
    [SerializeField] Controller controller;
    [SerializeField] float speed;

    Rigidbody _rb;

    Vector3 input;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        input = controller.GetMovementInput();
        //transform.position += controller.GetMovementInput() * speed * Time.deltaTime;
        if (input != Vector3.zero)
        {
            AddForce(controller.GetMovementInput() * speed);
            Move();
        } 
    }


}
