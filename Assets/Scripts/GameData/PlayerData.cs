using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] int _hearts;
    [SerializeField] int _maxHearts = 100;
    [SerializeField] int _cheetos;

    public int GetHearts()
    {
        return _hearts;
    }

    public void AddHearts(int val)
    {
        //_hearts += Mathf.Clamp(val, 0, _maxHearts);
        _hearts += val;
    }

    public void SetCheetos(int val)
    {
        _cheetos = val;
    }

    public int GetCheetos()
    {
        return _cheetos;
    }    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
