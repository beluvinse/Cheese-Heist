using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "My New Item", menuName = "CustomScriptable/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemCost;
    public Sprite itemImage;
    public ItemsRarity rarity;
}
