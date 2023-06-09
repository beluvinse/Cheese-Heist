using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] int _hearts;
    [SerializeField] int _maxHearts = 100;
    [SerializeField] int _cheetos;
    public SaveWithJson savedData;

    public int GetHearts()
    {
        return _hearts;
    }

    public void SetHearts(int val)
    {
         _hearts = val;
    }

    
    public int GetMaxHearts()
    {
        return _maxHearts;
    }

    public void AddHearts(int val)
    {
       _hearts = Mathf.Clamp(_hearts + val, 0, _maxHearts);
        savedData.SetHearts(_hearts);
    }

    public void SetCheetos(int val)
    {
        _cheetos = val;
        savedData.SetCheetos(_cheetos);
    }

    public void AddCheetos()
    {
        _cheetos++;
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

        _hearts = savedData.GetHearts();
        _cheetos = savedData.GetCheetos();
    }

}
