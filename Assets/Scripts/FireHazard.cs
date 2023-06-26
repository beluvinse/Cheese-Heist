using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    [SerializeField] float _durationTime;
    [SerializeField] float _waitingTime;

    void Update()
    {
        
    }


    IEnumerator Activate()
    {
         yield return new WaitForSeconds(_waitingTime);
    }
}
