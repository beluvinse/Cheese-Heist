using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerData playerData;

    public TMP_Text cheetosText;
    public TMP_Text cheetosText2;
    public TMP_Text heartsText;
    public TMP_Text heartsText2;
    int _maxHearts;

    private void Start()
    {
        _maxHearts = playerData.GetMaxHearts();
    }

    private void Update()
    {
        UpdateHearts();
        UpdateCheetos();
    }

    public void UpdateHearts()
    {
        var hearts = playerData.GetHearts();
        heartsText.text = "" + hearts;
        heartsText2.text = "" + hearts + "/" + _maxHearts;
    }

    public void UpdateCheetos()
    {
        var cheetos = playerData.GetCheetos();
        cheetosText.text = "" + cheetos;
        cheetosText2.text = "" + cheetos;
    }

}

