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
        SaveWithJson.Instance.SetHearts(_hearts); //no se si deberia hacerlo aca pero bueno
    }

    public void SetCheetos(int val)
    {
        _cheetos = val;
        SaveWithJson.Instance.SetCheetos(_cheetos);
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

    }

    private void Start()
    {
        _hearts = SaveWithJson.Instance.GetHearts();
        _cheetos = SaveWithJson.Instance.GetCheetos();
    }

}
