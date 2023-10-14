using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarketSlot : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    public Image itemImage;

    private IMItemInfo theIMItemInfo;

    void Start()
    {
        AddItem();
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
    }

    public void AddItem()
    {
        if (item != null)
        {
            itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            itemImage.color = item.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            itemImage.sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 1)
        {
            if (item != null)
            {
                if(item.itemName != theIMItemInfo.GetItem())
                {
                    ShowToolTip(item, transform.position);
                }
                else if (item.itemName == theIMItemInfo.GetItem())
                {
                    ShowToolTip(item, transform.position);
                }
            }
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theIMItemInfo.ShowToolTip(_item, _pos);
    }
}
