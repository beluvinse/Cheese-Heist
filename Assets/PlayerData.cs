using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] int _hearts;
    [SerializeField] int _maxHearts = 100;
    [SerializeField] int _cheetos;

    int getHearts()
    {
        return _hearts;
    }

    void setHearts(int val)
    {
        _hearts = Mathf.Clamp(val, 0, _maxHearts);
    }

    void setCheetos(int val)
    {
        _cheetos = val;
    }
}
