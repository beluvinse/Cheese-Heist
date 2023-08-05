using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{
    //public TMP_Text cheetosText;
    //public TMP_Text heartsText;
    public TMP_Text blueHeartsText;
    public TMP_Text potionText;
    public TMP_Text mouseText;

    public Button potionBtn;
    public Button mouseBtn;
    public Button reviveBtn;

    int _hearts;
    int _cheetos;
    int _blueHearts;
    int _potion;
    int _mouse;

    private void UpdateUI()
    {
        _hearts = PlayerData.Instance.GetHearts();
        _cheetos = PlayerData.Instance.GetCheetos();
        _blueHearts = PlayerData.Instance.GetBlueHearts();
        _potion = PlayerData.Instance.GetPotion();
        _mouse = PlayerData.Instance.GetMouse();

        //heartsText.text = "" + _hearts;
        //cheetosText.text = "" + _cheetos;
        blueHeartsText.text = "" + _blueHearts;
        potionText.text = "" + _potion;
        mouseText.text = "" + _mouse;
    }

    private void Start()
    {
        if (PlayerData.Instance.GetBlueHearts() <= 0)
            reviveBtn.interactable = false;

        if (PlayerData.Instance.GetPotion() <= 0)
            potionBtn.interactable = false;
        
        if (PlayerData.Instance.GetMouse() <= 0)
            mouseBtn.interactable = false;

        Debug.Log("potion: " + PlayerData.Instance.GetPotion());
        Debug.Log("mouse: " + PlayerData.Instance.GetMouse());


    }

    private void Update()
    {
        UpdateUI();
    }

    public void PotionButton()
    {
        if (PlayerData.Instance.GetPotion() > 0) //no hace falta en realidad poner esto
        {
            PlayerData.Instance.AddPotion(-1);
            UpdateUI();
        
        
        
        //te sube la velocidad (por un tiempo?)
        //desactivar el botton para que no se pueda tomar otra pocion hasta que termine el efecto
        
        }
        
    }

    public void MouseButton()
    {
        if (PlayerData.Instance.GetMouse() > 0)
        {
            PlayerData.Instance.AddMouse(-1);
            UpdateUI();
        
        
         //instancia mouse decoy
        
        }
       
    }


    

}
