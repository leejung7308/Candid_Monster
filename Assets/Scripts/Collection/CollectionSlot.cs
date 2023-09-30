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

    public Image collectionImage;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    public bool descActive = true;
    public GameObject descHideImage;

    void Start()
    {
        AddItem();
    }

    void Update()
    {
  
    }

    // 도감 획득
    public void AddItem()
    {
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
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

        descActive = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if (item != null)
            {
                if (descActive == false)
                {
                    descHideImage.SetActive(false);
                    ShowDesc(item);
                }
                else
                {
                    descHideImage.SetActive(true);
                }
            }
        }
    }

    public void ShowDesc(Item.Item _item)
    {
        collectionImage.sprite = _item.itemImage;
        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;
    }
}
