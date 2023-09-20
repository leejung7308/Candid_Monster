using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IMBuyButton : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;

    [SerializeField]
    private Inventory theInventory;

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
                theInventory.SetCoinText(item.itemValue);
                theInventory.AcquireItem(item);
            }
        }
    }
}
