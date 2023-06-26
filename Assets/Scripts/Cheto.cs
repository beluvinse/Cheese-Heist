using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cheto : MonoBehaviour, ICollectable
{
    [SerializeField] float value;

    public static event Action OnChetoCollected;

    public void Collect()
    {
        PlayerData.Instance.AddCheetos(1);
        Debug.Log("Yum, chetos");
        OnChetoCollected?.Invoke();
        Destroy(this.gameObject);
    }


    void Update()
    {
        transform.Rotate(Vector3.up * value * Time.deltaTime, Space.Self);
    }
}
