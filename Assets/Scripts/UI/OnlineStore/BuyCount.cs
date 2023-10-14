using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuyCount : MonoBehaviour, IPointerClickHandler
{
    public int buyCount;
    public Item.Item item;
    int Value;

    public GameObject PlusButton;
    public GameObject MinusButton;

    [SerializeField]
    public TextMeshProUGUI text_Count;
    [SerializeField]
    public TextMeshProUGUI text_Purchase;

    public void SetBuyItem(Item.Item _item)
    {
        item = _item;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (eventData.clickCount == 1)
        //{
            if (eventData.pointerCurrentRaycast.gameObject == PlusButton)
            {
                UpdateBuyCount(1);
            }
            else if(eventData.pointerCurrentRaycast.gameObject == MinusButton)
            {
                UpdateBuyCount(-1);
            }
            Value = item.itemValue * buyCount;
            text_Purchase.text = Value.ToString();
        //}
    }

    public void UpdateBuyCount(int _buyCount)
    {
        buyCount += _buyCount;
        if (buyCount <= 0)
        {
            buyCount = 0;
        }
        text_Count.text = buyCount.ToString();
    }

    public int GetBuyCount()
    {
        return buyCount;
    }

    public void SetBuyCount()
    {
        buyCount = 0;
        Value = 0;
        text_Purchase.text = Value.ToString();
        text_Count.text = buyCount.ToString();
    }
}
