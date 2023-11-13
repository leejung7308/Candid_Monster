using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CollectionSlot : MonoBehaviour, IPointerClickHandler
{
    public Item.Item item;
    public Image itemImage;
    public Image itemHideImage;

    private CollectionDesc theCollectionDesc;
    private Inventory theInventory;

    void Start()
    {
        Additem();
        theCollectionDesc = FindObjectOfType<CollectionDesc>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        UnlockItem();
    }

    private void Additem()
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

    public void UnlockItem()
    {
        string[][] EquipUnlock = theInventory.AcquireEquip;
        string[][] ConsumableUnlock = theInventory.AcquireConsumable;
        for (int i = 0; i < theInventory.AcquireEquip.Length; i++)
        {
            if (item != null && EquipUnlock[i][0] == item.itemName && EquipUnlock[i][1] == "1")
            {
                SetColor(0);
            }
        }
        for (int i = 0; i < theInventory.AcquireConsumable.Length; i++)
        {
            if (item != null && ConsumableUnlock[i][0] == item.itemName && ConsumableUnlock[i][1] == "1")
            {
                SetColor(0);
            }
        }
    }

    private void SetColor(float _alpha)
    {
        Color color = itemHideImage.color;
        color.a = _alpha;
        itemHideImage.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if(item != null)
            {
                if (itemHideImage.color.a == 0)
                {
                    ShowDesc(item, transform.position);
                }
                else
                {
                    UnKnownDesc(item, transform.position);
                }
            }
        }
    }

    public void ShowDesc(Item.Item _item, Vector3 _pos)
    {
        theCollectionDesc.ShowDesc(_item, _pos);
    }

    public void UnKnownDesc(Item.Item _item, Vector3 _pos)
    {
        theCollectionDesc.UnknownDesc(_item, _pos);
    }
}
