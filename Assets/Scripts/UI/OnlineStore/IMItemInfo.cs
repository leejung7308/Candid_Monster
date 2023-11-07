using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IMItemInfo : MonoBehaviour
{
    [SerializeField]
    private IMBuyButton theIMBuyButton;
    [SerializeField]
    private BuyCount theBuyCount;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {   
        theIMBuyButton.SetBuyItem(_item);
        theBuyCount.SetBuyItem(_item);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;

        theBuyCount.SetBuyCount();
    }

    public string GetItem()
    {
        return txt_ItemName.text;
    }
}
