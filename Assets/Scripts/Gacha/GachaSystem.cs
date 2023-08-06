using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    [SerializeField] GachaPool[] myPool = new GachaPool[0];
    [SerializeField] int pitySystem = 20;
    [SerializeField] int _gachaCost;

    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] TextMeshProUGUI _itemQty;
    [SerializeField] Image _itemImage;
    [SerializeField] GameObject _winPanel;

    [SerializeField] UIManager _uiManager;

    float totalChance;
    int pullCount;

    void Start()
    {
        totalChance = 0;

        for (int i = 0; i < myPool.Length; i++)
        {
            totalChance += myPool[i].appearsChance;
            for (int e = 0; e < myPool[i].myItems.Length; e++)
            {
                myPool[i].myItems[e].rarity = myPool[i].rarity;
            }
        }
    }

    public void PullGachaOnce()
    {
        if (PlayerData.Instance.GetCheetos() >= 1)
        {
            PullGachaSystem(1);
        }
    }

    public void PullGachaSystem(int pullNumber)
    {
        for (int i = 0; i < pullNumber; i++)
        {
            var item = GetItem();
            PlayerData.Instance.AddCheetos(-_gachaCost);
            _uiManager.UpdateCheetos();
            var n = Random.Range(10, 100);
            switch (item.itemName)
            {
                case "Cheetos":
                    PlayerData.Instance.AddCheetos(n);
                    break;
                case "Hearts":
                    PlayerData.Instance.AddHearts(10);
                    break;
                case "Blue Heart":
                    PlayerData.Instance.AddBlueHeart(1);
                    break;
                case "Mouse Decoy":
                    PlayerData.Instance.AddMouse(1);
                    break;
                case "Potion":
                    PlayerData.Instance.AddPotion(1);
                    break;
            }
            SetItem(item, n);
            _winPanel.SetActive(true);
            Debug.Log("El gacha te obsequio: " + item.itemName + " de rareza " + item.rarity);
            SaveWithJson.Instance.SaveGame();
        }
    }

    public void SetItem(Item myItem, int n)
    {
        _itemName.text = myItem.itemName;
        if(myItem.itemName == "Cheetos")
            _itemQty.text = n.ToString();
        else
            _itemQty.text = myItem.itemCost;
        _itemImage.sprite = myItem.itemImage;
    }

    private Item GetItem()
    {
        GachaPool temPool = null;
        pullCount++;

        if (pullCount >= pitySystem)
        {
            temPool = myPool[myPool.Length - 1];
            Debug.Log("Aparece gracias al Pity System. no tengo suerte");
        }
        else
        {
            float randomValue = Random.Range(0, totalChance);

            for (int i = 0; i < myPool.Length; i++)
            {
                randomValue -= myPool[i].appearsChance;
                if (randomValue <= 0)
                {
                    temPool = myPool[i];
                    break;
                }
            }
        }

        if (temPool.rarity == ItemsRarity.legendary)
        {
            pullCount = 0;
        }

        int RandomItem = Random.Range(0, temPool.myItems.Length);

        return temPool.myItems[RandomItem];
    }
}
