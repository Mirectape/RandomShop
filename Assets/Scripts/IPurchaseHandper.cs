using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPurchaseHandper
{
    /// <summary>
    /// To check if adding gems or coins make a product avaiable or not
    /// </summary>
    /// <param name="currencyTypeValue">currency money client has</param>
    /// <param name="currencyCostValue">currency money product costs</param>
    /// <param name="buttonToPurchase">button to activate/disactivate</param>
    public void CheckPurchasable(int currencyTypeValue, int currencyCostValue, Button buttonToPurchase);

    public void PurchaseItemByGems(int itemNumberInArray);

    public void PurchaseItemByCoins(int itemNumberInArray);
}
