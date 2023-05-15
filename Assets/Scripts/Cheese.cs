using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour, ICollectable
{
    public static event Action OnCheeseCollected;

    public void Collect()
    {
        OnCheeseCollected?.Invoke();
        Destroy(this);
    }
}
