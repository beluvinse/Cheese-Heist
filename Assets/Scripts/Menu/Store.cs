using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    int _cheetos;
    int _hearts;
    int _maxHearts;
    public UIManager uiManager;

    [SerializeField] Button _1heartBtn;
    [SerializeField] Button _5heartBtn;
    [SerializeField] Button _20heartBtn;
    [SerializeField] Button _chestBtn;

    private void GetData()
    {    
        _cheetos = PlayerData.Instance.GetCheetos(); 
        _hearts = PlayerData.Instance.GetHearts();
    }

    private void Start()
    {
        _maxHearts = PlayerData.Instance.GetMaxHearts();
    }

    private void Update()
    {
        var cheetos = PlayerData.Instance.GetCheetos();

        if (cheetos < 10)
            _1heartBtn.interactable = false;
        else 
            _1heartBtn.interactable = true;

        if (cheetos < 50)
        {
            _5heartBtn.interactable = false;
            _chestBtn.interactable = false;
        }
        else
        {
            _5heartBtn.interactable = true;
            _chestBtn.interactable = true;
        }

        if (cheetos < 80)
            _20heartBtn.interactable = false;
        else
            _20heartBtn.interactable = true;


    }

    public void Buy1Heart()
    {
        GetData();

        if(_cheetos >= 10)
        {
            PlayerData.Instance.AddHearts(1);
            PlayerData.Instance.SetCheetos(_cheetos - 10);
            uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }

    }
    
    public void Buy5Heart()
    {
        GetData();

        if (_cheetos >= 50)
        {
            PlayerData.Instance.AddHearts(5);
            PlayerData.Instance.SetCheetos(_cheetos - 50);
            uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }
    }
    
    public void Buy20Heart()
    {
        GetData();

        if (_cheetos >= 80)
        {
            PlayerData.Instance.AddHearts(20);
            PlayerData.Instance.SetCheetos(_cheetos - 80);
            uiManager.UpdateCheetos();
            uiManager.UpdateHearts();
        }
    }
}
