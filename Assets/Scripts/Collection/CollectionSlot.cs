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

    public static bool descActivated = true;
    public GameObject descHideImage;

    void Start()
    {
        AddItem();
    }

    void Update()
    {
  
    }

    // µµ°¨ È¹µæ
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
        //SetColor(0);
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
                if (descActivated == true)
                {
                    descHideImage.SetActive(false);
                    ShowDesc(item);
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
