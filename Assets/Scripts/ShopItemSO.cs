using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// To assign types of Shop Items to classes inheriting IShopItemInfo  
/// </summary>
enum ItemType { PenKnife, Handgun, Shotgun }

[CreateAssetMenu(fileName = "ShopItem",
    menuName = "Scriptable Objects/Create New Shop Item",
    order = 1)]

public class ShopItemSO : ScriptableObject, IShopItemInfo
{
    public Sprite itemImage;
    public int gemsCost;
    public int coinsCost;

    [SerializeField] private ItemType _itemType;

    public int GetItemType()
    {
        switch (_itemType)
        {
            case ItemType.PenKnife:
                return 0;
            case ItemType.Handgun:
                return 1;
            case ItemType.Shotgun:
                return 2;
            default:
                return -1;
        }
    }
}
