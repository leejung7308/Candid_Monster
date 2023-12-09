using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OnlineMarketSlot : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    public Image itemImage;

    private OnItemToolTip theOnItemToolTip;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemValue;

    void Start()
    {
        AddItem();
        theOnItemToolTip = FindObjectOfType<OnItemToolTip>();
    }

    public void AddItem()
    {
        if (item != null)
        {
            itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            itemImage.color = item.GetComponent<SpriteRenderer>().color;
            txt_ItemName.text = item.itemName;
            txt_ItemValue.text = item.itemValue.ToString();
        }
        else
        {
            itemImage.sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if (item != null)
            {
                if (item.itemName != theOnItemToolTip.GetItem())
                {
                    ShowToolTip(item, transform.position);
                }
                else if (item.itemName == theOnItemToolTip.GetItem())
                {
                    ShowToolTip(item, transform.position);
                }
            }
        }
    }

    public void ShowToolTip(Item.Item _item, Vector3 _pos)
    {
        theOnItemToolTip.ShowToolTip(_item, _pos);
    }
}
