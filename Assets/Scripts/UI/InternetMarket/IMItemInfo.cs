using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IMItemInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private IMBuyButton theIMBuyButton;
    [SerializeField]
    private BuyCount theBuyCount;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;
    [SerializeField]
    private TextMeshProUGUI txt_ItemValue;

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {   
        go_Base.SetActive(true);
        theIMBuyButton.SetBuyItem(_item);
        theBuyCount.SetBuyItem(_item);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        txt_ItemValue.text = _item.itemValue.ToString();

        theBuyCount.SetBuyCount();
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }

    public string GetItem()
    {
        return txt_ItemName.text;
    }

    public bool GetToolTipActive()
    {
        return go_Base.activeSelf;
    }
}
