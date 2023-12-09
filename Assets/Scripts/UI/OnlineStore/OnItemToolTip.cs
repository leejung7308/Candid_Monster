using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnItemToolTip : MonoBehaviour
{
    [SerializeField]
    private OnBuyButton theOnBuyButton;
    [SerializeField]
    private OnBuyCount theOnBuyCount;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;
    public GameObject DeliveryFee;

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theOnBuyButton.SetBuyItem(_item);
        theOnBuyCount.SetBuyItem(_item);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
        DeliveryFee.SetActive(true);

        theOnBuyCount.SetBuyCount();
    }

    public string GetItem()
    {
        return txt_ItemName.text;
    }
}
