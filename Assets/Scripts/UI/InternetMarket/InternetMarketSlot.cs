using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarketSlot : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item; //등록할 아이템
    public Image itemImage;  // 아이템의 이미지

    private IMItemInfo theIMItemInfo;

    [SerializeField]
    private Inventory theInventory;

    void Start()
    {
        AddItem();
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
    }

    // 상점에 새로운 아이템 슬롯 추가
    public void AddItem()
    {
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
        }
        else
        {
            itemImage.sprite = null;
        }
    }

    //상점 설명창
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
                    if (theIMItemInfo.GetToolTipActive() == false)
                    {
                        ShowToolTip(item, transform.position);
                    }
                    else HideToolTip();
                }
            }
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theIMItemInfo.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        theIMItemInfo.HideToolTip();
    }
}
