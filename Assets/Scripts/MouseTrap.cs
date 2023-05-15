using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{

    [SerializeField] float root;


    private void OnTriggerEnter(Collider other)
    {
        var mouse = other.GetComponent<MouseController>();

        if(mouse != null)
        {
            mouse.Trapped(root);
        }
    }
}
