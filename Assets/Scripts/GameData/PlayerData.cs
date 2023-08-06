using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] int _hearts;
    [SerializeField] int _maxHearts = 100;
    [SerializeField] int _cheetos;
    [SerializeField] int _specialHeart;
    [SerializeField] int _decoyMouse;
    [SerializeField] int _potion;

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
        _hearts = _hearts + val;
    }

    public void SetCheetos(int val)
    {
        _cheetos = val;
    }

    public void AddCheetos(int val)
    {
        _cheetos += val;
    }

    public int GetCheetos()
    {
        return _cheetos;
    }

    public int GetPotion()
    {
        return _potion;
    }

    public int GetMouse()
    {
        return _decoyMouse;
    }

    public int GetBlueHearts()
    {
        return _specialHeart;
    }

    public void AddBlueHeart(int val)
    {
        _specialHeart += val;
    }

    public void AddPotion(int val)
    {
        _potion += val;
    }

    public void AddMouse(int val)
    {
        _decoyMouse += val;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _hearts = SaveWithJson.Instance.GetHearts();
        _cheetos = SaveWithJson.Instance.GetCheetos();
        _potion = SaveWithJson.Instance.GetPotion();
        _decoyMouse = SaveWithJson.Instance.GetMouse();
        _specialHeart = SaveWithJson.Instance.GetBlueHearts();
    }

}
