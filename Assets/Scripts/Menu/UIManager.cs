using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text cheetosText;
    public TMP_Text cheetosText2;
    public TMP_Text heartsText;
    public TMP_Text heartsText2;
    //public TMP_Text timerText;
    //public TMP_Text timerText2;
    
    int _maxHearts;
    [SerializeField] int _hearts;
    [SerializeField] int _cheetos;

    private void Awake()
    {
        StartCoroutine(UpdateUI());
    }

    private IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(.2f);
        _maxHearts = PlayerData.Instance.GetMaxHearts();
        _hearts = PlayerData.Instance.GetHearts();
        _cheetos = PlayerData.Instance.GetCheetos();
        UpdateCheetos();
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        _hearts = PlayerData.Instance.GetHearts();

        heartsText.text = "" + _hearts;
        heartsText2.text = "" + _hearts + "/" + _maxHearts;

        /*if (_hearts >= _maxHearts)
        {
            timerText.text = ("Full!");
            timerText2.text = ("Full!");
        }
        else*/

    }

    public void UpdateCheetos()
    {
        _cheetos = PlayerData.Instance.GetCheetos();

        cheetosText.text = "" + _cheetos;
        cheetosText2.text = "" + _cheetos;
    }

}

