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
        string[][] itemunlock = theInventory.Acquire;
        for(int i = 0; i < theInventory.Acquire.Length; i++)
        {
            if (item != null && itemunlock[i][0] == item.itemName && itemunlock[i][1] == "1")
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
            if (item != null && itemHideImage.color.a == 0)
            {
                ShowDesc(item, transform.position);
            }
        }
    }

    public void ShowDesc(Item.Item _item, Vector3 _pos)
    {
        theCollectionDesc.ShowDesc(_item, _pos);
    }
}
