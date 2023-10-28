using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OffStoreSlot : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    public Image itemImage;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemValue;

    private Inventory theInventory;

    void Start()
    {
        AddItem();
        theInventory = FindObjectOfType<Inventory>();
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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                if (item.itemValue <= int.Parse(theInventory.GetCoinText()))
                {
                    if (item.itemType== Item.ItemType.Ingredient)
                    {
                        theInventory.SetCoinText(item.itemValue);
                        theInventory.AcquireItem(item);
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        theInventory.SetCoinText(item.itemValue);
                        theInventory.AcquireItem(item);
                    }
                }
            }
        }

    }
}
