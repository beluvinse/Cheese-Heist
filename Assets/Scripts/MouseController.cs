using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] float speed;

    void Update()
    {
        transform.position += controller.GetMovementInput() * speed * Time.deltaTime;
    }
}
