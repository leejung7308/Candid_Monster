using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class IMBuyButton : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    int purchaseCost;

    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    private BuyCount theBuyCount;

    private void Start()
    {
        theBuyCount = FindObjectOfType<BuyCount>();
    }

    public void SetBuyItem(Item.Item _item)
    {
        item = _item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if (item != null)
            {
                for (int i = 0; i < theBuyCount.GetBuyCount(); i++)
                {
                    purchaseCost += item.itemValue;
                }
                if (purchaseCost <= int.Parse(theInventory.GetCoinText()))
                {
                    for (int i = 0; i < theBuyCount.GetBuyCount(); i++)
                    {
                        theInventory.SetCoinText(item.itemValue);
                        theInventory.AcquireItem(item);
                    }
                    theBuyCount.SetBuyCount();
                }
                purchaseCost = 0;
            }
        }
    }
}
