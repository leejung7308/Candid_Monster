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

    void Start()
    {
        AddItem();
        theCollectionDesc = FindObjectOfType<CollectionDesc>();
    }

    // 도감 획득
    public void AddItem()
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
        AcquireItem();
    }

    public void AcquireItem()
    {
        //도감 아이템 해금 조건 추가
        SetColor(0);
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
            if (item != null)
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
