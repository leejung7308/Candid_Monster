using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class IMBuyButton : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    int purchaseCost=3000;

    private Inventory theInventory;
    private BuyCount theBuyCount;

    public GameObject Parcel;

    private void Start()
    {
        theBuyCount = FindObjectOfType<BuyCount>();
        theInventory = FindObjectOfType<Inventory>();
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
                    OnParcel();
                    theInventory.SetCoinText(3000);
                    for (int i = 0; i < theBuyCount.GetBuyCount(); i++)
                    {
                        theInventory.SetCoinText(item.itemValue);
                        theInventory.AcquireItem(item);
                    }
                    theBuyCount.SetBuyCount();
                }
                purchaseCost = 3000;
            }
        }
    }

    private void OnParcel()
    {
        Parcel.SetActive(true);
        Invoke("HideParcel", 1);
    }
    private void HideParcel()
    {
        Parcel.SetActive(false);
    }
}
