using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    public Image itemImage;
    public TMP_Text gemsCost;
    public TMP_Text coinsCost;
    public Button purchaseByGemsButton;
    public Button purchaseByCoinsButton;
    public Button viewProductButton;
    public bool isPurchased = false;

    [SerializeField] private ItemType _telplateType; 
    public int GetItemType()
    {
        switch (_telplateType)
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
