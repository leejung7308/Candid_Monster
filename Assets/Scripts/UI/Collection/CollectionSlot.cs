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
        theCollectionDesc = FindObjectOfType<CollectionDesc>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {

    }

    public void AddItem(Item.Item _item)
    {
        item = _item;
        itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        itemImage.color = item.GetComponent<SpriteRenderer>().color;
        if (_item.itemAcquire == 1)
        {
            SetColor(0);
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
