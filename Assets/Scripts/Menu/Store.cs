using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    int _cheetos;
    int _hearts;
    int _maxHearts;
    public UIManager uiManager;

    private void GetData()
    {    
        _cheetos = PlayerData.Instance.GetCheetos(); 
        _hearts = PlayerData.Instance.GetHearts();
    }

    private void Start()
    {
        _maxHearts = PlayerData.Instance.GetMaxHearts();
    }

    public void Buy1Heart()
    {
        GetData();

        if(_hearts < _maxHearts && _cheetos >= 1)
        {
            PlayerData.Instance.AddHearts(1);
            PlayerData.Instance.SetCheetos(_cheetos - 1);
            uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }

    }
    
    public void Buy5Heart()
    {
        GetData();

        if (_hearts < _maxHearts && _cheetos >= 5)
        {
            PlayerData.Instance.AddHearts(5);
            PlayerData.Instance.SetCheetos(_cheetos - 5);
            uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }
    }
    
    public void Buy20Heart()
    {
        GetData();

        if (_hearts < _maxHearts && _cheetos >= 15)
        {
            PlayerData.Instance.AddHearts(20);
            PlayerData.Instance.SetCheetos(_cheetos - 15);
            uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }
    }

    
}
