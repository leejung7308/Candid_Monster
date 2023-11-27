using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnBuyButton : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    int purchaseCost;

    private Inventory theInventory;
    private OnBuyCount theOnBuyCount;

    public GameObject Parcel;

    private void Start()
    {
        theOnBuyCount = FindObjectOfType<OnBuyCount>();
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
                for (int i = 0; i < theOnBuyCount.GetBuyCount(); i++)
                {
                    purchaseCost += item.itemValue;
                }
                if (purchaseCost <= int.Parse(theInventory.GetCoinText()))
                {
                    OnParcel();
                    for (int i = 0; i < theOnBuyCount.GetBuyCount(); i++)
                    {
                        theInventory.SetCoinText(item.itemValue);
                        theInventory.AcquireItem(item);
                    }
                    theOnBuyCount.SetBuyCount();
                }
                purchaseCost = 0;
            }
        }
    }

    private void OnParcel()
    {
        Parcel.SetActive(true);
        Invoke("HideParcel",1);
    }

    private void HideParcel()
    {
        Parcel.SetActive(false);
    }
}
