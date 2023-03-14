using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour, IMoneyGenerator, IPurchaseHandper
{
    [SerializeField] private int _totalClientGems;
    [SerializeField] private int _totalClientCoins;
    [SerializeField] private TMP_Text _gemsUI;
    [SerializeField] private TMP_Text _coinsUI;
    [SerializeField] private ShopItemSO[] _shopItemsScriptable; // To contain all the scriptable objects from the project
    [SerializeField] private ShopTemplate[] _shopTemplates; // To contain the info of all the purchasable shop items from the project

    private int[] _shopTemplatesPurchased; // to contain data about if product is purchased 1 - yes 0 -no

    private void Start()
    {
        _gemsUI.text = "GEMS: " + _totalClientGems.ToString();
        _coinsUI.text = "COINS: " + _totalClientCoins.ToString();
        _shopTemplatesPurchased = new int[_shopTemplates.Length];
        LoadTemplateInfo(); // upload data from repos as we start the app
        for (int i = 0; i < _shopTemplates.Length; i++)
        {
            for(int j = 0; j < _shopItemsScriptable.Length; j++)
            {
                if (_shopTemplates[i].isPurchased && _shopTemplates[i].GetItemType() == _shopItemsScriptable[j].GetItemType())
                {
                    _shopTemplates[i].itemImage.sprite = _shopItemsScriptable[j].itemImage;
                }
            }
        }
    }

    public void AddGems()
    {
        _totalClientGems += 5;
        _gemsUI.text = "GEMS: " + _totalClientGems.ToString();
        CheckPurchasableByGems();
        SaveSystem.SaveClient(this); //save after each operation
    }

    public void AddCoins()
    {
        _totalClientCoins += 10;
        _coinsUI.text = "COINS: " + _totalClientCoins.ToString();
        CheckPurchasableByCoins();
        SaveSystem.SaveClient(this);
    }

    /// <summary>
    /// We use this func to go throuth the array of products
    /// </summary>
    public void CheckPurchasableByCoins()
    {
        for (int i = 0; i < _shopTemplates.Length; i++)
        {
            for (int j = 0; j < _shopItemsScriptable.Length; j++)
            {
                if (_shopTemplates[i].GetItemType() == _shopItemsScriptable[j].GetItemType() && !_shopTemplates[i].isPurchased)
                {
                    CheckPurchasable(_totalClientCoins, _shopItemsScriptable[j].coinsCost, _shopTemplates[i].purchaseByCoinsButton);
                }
            }
            if (_shopTemplates[i].isPurchased)
            {
                _shopTemplates[i].purchaseByGemsButton.interactable = false;
                _shopTemplates[i].purchaseByCoinsButton.interactable = false;
                _shopTemplates[i].viewProductButton.interactable = false;
            }
        }
    }

    public void CheckPurchasableByGems()
    {
        for (int i = 0; i < _shopTemplates.Length; i++)
        {
            for (int j = 0; j < _shopItemsScriptable.Length; j++)
            {
                if (_shopTemplates[i].GetItemType() == _shopItemsScriptable[j].GetItemType() && !_shopTemplates[i].isPurchased)
                {
                    CheckPurchasable(_totalClientGems, _shopItemsScriptable[j].gemsCost, _shopTemplates[i].purchaseByGemsButton);
                }
            }
            if (_shopTemplates[i].isPurchased)
            {
                _shopTemplates[i].purchaseByGemsButton.interactable = false;
                _shopTemplates[i].purchaseByCoinsButton.interactable = false;
                _shopTemplates[i].viewProductButton.interactable = false;
            }
        }
    }

    public void CheckPurchasable(int currencyTypeValue, int currencyCostValue, Button buttonToPurchase)
    {
        if (currencyTypeValue >= currencyCostValue)
        {
            buttonToPurchase.interactable = true;
        }
        else
        {
            buttonToPurchase.interactable = false;
        }
    }

    /// <summary>
    /// We take info here from our scriptable objects into shopItems AKA shopTemplates
    /// </summary>
    public void LoadTemplateInfo()
    {
        if(SaveSystem.LoadClient() != null)
        {
            _totalClientGems = SaveSystem.LoadClient().totalClientGems;
            _totalClientCoins = SaveSystem.LoadClient().totalClientCoins;
            for (int i = 0; i < _shopTemplates.Length; i++)
            {
                _shopTemplatesPurchased[i] = SaveSystem.LoadClient().isItemPurchased[i];
                _shopTemplates[i].isPurchased = _shopTemplatesPurchased[i] == 1 ? true : false;
            }
        }
        
        for (int i = 0; i < _shopTemplates.Length; i++)
        {
            for (int j = 0; j < _shopItemsScriptable.Length; j++)
            {
                if (_shopTemplates[i].GetItemType() == _shopItemsScriptable[j].GetItemType())
                {
                    _shopTemplates[i].gemsCost.text = _shopItemsScriptable[j].gemsCost.ToString();
                    _shopTemplates[i].coinsCost.text = _shopItemsScriptable[j].coinsCost.ToString();
                }
            }
        }
        _coinsUI.text = "COINS: " + _totalClientCoins.ToString();
        _gemsUI.text = "GEMS: " + _totalClientGems.ToString();

        CheckPurchasableByCoins();
        CheckPurchasableByGems();

    }

    public void PurchaseItemByGems(int itemNumberInArray)
    {
        for (int j = 0; j < _shopItemsScriptable.Length; j++)
        {
            if (_shopTemplates[itemNumberInArray].GetItemType() == _shopItemsScriptable[j].GetItemType())
            {
                _totalClientGems = _totalClientGems - _shopItemsScriptable[j].gemsCost;
                _shopTemplates[itemNumberInArray].isPurchased = true;
                _shopTemplates[itemNumberInArray].itemImage.sprite = _shopItemsScriptable[j].itemImage;
            }
        }
        _gemsUI.text = "GEMS: " + _totalClientGems.ToString();
        CheckPurchasableByGems();
        SaveSystem.SaveClient(this);
    }

    public void PurchaseItemByCoins(int itemNumberInArray)
    {
        for (int j = 0; j < _shopItemsScriptable.Length; j++)
        {
            if (_shopTemplates[itemNumberInArray].GetItemType() == _shopItemsScriptable[j].GetItemType())
            {
                _totalClientCoins = _totalClientCoins - _shopItemsScriptable[j].coinsCost;
                _shopTemplates[itemNumberInArray].isPurchased = true;
                _shopTemplates[itemNumberInArray].itemImage.sprite = _shopItemsScriptable[j].itemImage;
            }
        }
        _coinsUI.text = "COINS: " + _totalClientCoins.ToString();
        CheckPurchasableByCoins();
        SaveSystem.SaveClient(this);
    }

    public void DemonstrateItem(int itemNumberInArray)
    {
        StartCoroutine(WaitForSeconds(2f, itemNumberInArray));
    }

    private IEnumerator WaitForSeconds(float seconds, int itemNumberInArray) 
    {
        for (int j = 0; j < _shopItemsScriptable.Length; j++)
        {
            if (_shopTemplates[itemNumberInArray].GetItemType() == _shopItemsScriptable[j].GetItemType())
            {
                Sprite tempSprite = _shopTemplates[itemNumberInArray].itemImage.sprite;
                _shopTemplates[itemNumberInArray].itemImage.sprite = _shopItemsScriptable[j].itemImage;
                yield return new WaitForSeconds(seconds);
                _shopTemplates[itemNumberInArray].itemImage.sprite = tempSprite;
            }
        }        
    }

    public int GetTotalClientGems() => _totalClientGems;
    public int GetTotalClientCoins() => _totalClientCoins;
    public int[] GetPurchasedItemInfo()
    {
        for (int i = 0; i < _shopTemplates.Length; i++)
        {
            _shopTemplatesPurchased[i] = _shopTemplates[i].isPurchased ? 1 : 0;
        }
        return _shopTemplatesPurchased;
    }

    public ShopTemplate[] GetShopTemplates() => _shopTemplates;

}
