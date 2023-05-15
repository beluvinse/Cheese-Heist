using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public PlayerData playerData;
    int _cheetos;
    int _hearts;
    int _maxHearts;
    public UIManager uiManager;

    private void GetData()
    {    
        _cheetos = playerData.GetCheetos(); 
        _hearts = playerData.GetHearts();
        _maxHearts = playerData.GetMaxHearts();
    }

    public void Buy1Heart()
    {
        GetData();

        if(_hearts < _maxHearts && _cheetos >= 1)
        {
            playerData.AddHearts(1);
            playerData.SetCheetos(_cheetos - 1);
            //uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }

    }
    
    public void Buy5Heart()
    {
        GetData();

        if (_hearts < _maxHearts && _cheetos >= 5)
        {
            playerData.AddHearts(5);
            playerData.SetCheetos(_cheetos - 5);
        }
    }
    
    public void Buy20Heart()
    {
        GetData();

        if (_hearts < _maxHearts && _cheetos >= 15)
        {
            playerData.AddHearts(20);
            playerData.SetCheetos(_cheetos - 15);
        }
    }

    
}
