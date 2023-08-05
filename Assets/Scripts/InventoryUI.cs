using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text cheetosText;
    public TMP_Text heartsText;
    public TMP_Text blueHeartsText;
    public TMP_Text potionText;
    public TMP_Text mouseText;

    int _hearts;
    int _cheetos;
    int _blueHearts;
    int _potion;
    int _mouse;


    private void Update()
    {
        _hearts = PlayerData.Instance.GetHearts();
        _cheetos = PlayerData.Instance.GetCheetos();
        _blueHearts = PlayerData.Instance.GetBlueHearts();
        _potion = PlayerData.Instance.GetPotion();
        _mouse = PlayerData.Instance.GetMouse();

        heartsText.text = "" + _hearts;
        cheetosText.text = "" + _cheetos;
        blueHeartsText.text = "" + _blueHearts;
        potionText.text = "" + _potion;
        mouseText.text = "" + _mouse;
    }
}
