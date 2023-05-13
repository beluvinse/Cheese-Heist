using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheto : MonoBehaviour
{
    [SerializeField] float value;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * value * Time.deltaTime, Space.Self);
    }
}
