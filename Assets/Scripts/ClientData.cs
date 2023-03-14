using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientData
{
    public int totalClientGems;
    public int totalClientCoins;
    public int[] isItemPurchased;

    public ClientData(ShopManager shopManager)
    {
        totalClientGems = shopManager.GetTotalClientGems();
        totalClientCoins = shopManager.GetTotalClientCoins();
        isItemPurchased = shopManager.GetPurchasedItemInfo();
    }
}