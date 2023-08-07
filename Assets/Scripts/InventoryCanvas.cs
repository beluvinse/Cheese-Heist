using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{

    public TMP_Text blueHeartsText;
    public TMP_Text potionText;
    public TMP_Text mouseText;

    public Button potionBtn;
    public Button mouseBtn;
    public Button reviveBtn;

    int _blueHearts;
    int _potion;
    int _mouse;

    [SerializeField] MouseController _mouseCon;

    private void UpdateUI()
    {
        _blueHearts = PlayerData.Instance.GetBlueHearts();
        _potion = PlayerData.Instance.GetPotion();
        _mouse = PlayerData.Instance.GetMouse();

        blueHeartsText.text = "" + _blueHearts;
        potionText.text = "" + _potion;
        mouseText.text = "" + _mouse;


        if (_blueHearts <= 0)
            reviveBtn.interactable = false;

        if (_potion <= 0)
            potionBtn.interactable = false;

        if (_mouse <= 0)
            mouseBtn.interactable = false;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    public void PotionButton()
    {
        _mouseCon.SpeedBoost();
        PlayerData.Instance.AddPotion(-1);
        UpdateUI();
    }

    public void MouseButton()
    {
        _mouseCon.SpawnDecoy();
        PlayerData.Instance.AddMouse(-1);
        UpdateUI();
    }




}
