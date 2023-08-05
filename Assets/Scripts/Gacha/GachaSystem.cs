using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [SerializeField] GachaPool[] myPool = new GachaPool[0];
    [SerializeField] int pitySystem = 20;
    [SerializeField] int _gachaCost;

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

    public void PullGacha(int pullNumber)
    {
        for (int i = 0; i < pullNumber; i++)
        {
            var item = GetItem();
            PlayerData.Instance.AddCheetos(-_gachaCost); //ACA PONER CUANTO CUESTA CADA CAJA
            _uiManager.UpdateCheetos();
            Debug.Log("El gacha te obsequio: " + item.itemName + " de rareza " + item.rarity);
        }
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
