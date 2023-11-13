using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private bool isDoubleClick = false;
    private float doubleClickTimeThreshold = 0.3f;

    public Item.Item item;
    private int itemCount;
    public Image itemImage;
    [SerializeField]
    private GameObject go_CountImage;
    [SerializeField]
    private TextMeshProUGUI text_Count;

    private Player thePlayer;
    private ItemInfo theItemInfo;
    private Inventory theInventory;
    private Storage theStorage;

    private Rect baseRect; 

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        theInventory = FindObjectOfType<Inventory>();
        theStorage = FindObjectOfType<Storage>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    public void AddItem(Item.Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite; 
        itemImage.color = item.GetComponent<SpriteRenderer>().color;

        if (item.itemType != Item.ItemType.Equipment && item.itemType != Item.ItemType.ETC)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else if(item.itemType == Item.ItemType.Equipment) 
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        else
        {

        }

        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
        HideInfo();
    }
    
    private IEnumerator ResetDoubleClickFlag()
    {
        yield return new WaitForSeconds(doubleClickTimeThreshold);
        isDoubleClick = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            isDoubleClick = true;
            StartCoroutine(ResetDoubleClickFlag());
        }

        if (theInventory.CheckGolfStore() == true || theInventory.CheckWineStore() == true || theInventory.CheckSmokeStore() == true || theInventory.CheckCoffeeStore() == true || theInventory.CheckConvienceStore()==true)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (item != null)
                {
                    theInventory.SetCoinText(-(item.itemValue / 2)); //아이템 판매
                    SetSlotCount(-1);
                }
            }
        }

        if (isDoubleClick)
        {
            if(item != null)
            {
                if (theInventory.CheckStorage() == true)
                {
                    for(int i = 0; i < itemCount; i++)
                    {
                        theStorage.AcquireItem(item);
                    }
                    ClearSlot();
                }
                else 
                {
                    if (item.itemType == Item.ItemType.Equipment)
                    {
                        if (thePlayer != null)
                        {
                            thePlayer.EquipItem(item.itemName); //아이템 장착
                        }
                    }
                    else
                    {
                        //아이템 소모
                        Debug.Log("used item");
                        SetSlotCount(-1);
                    }
                }
            }  
        }
    }

    public void ShowInfo(Item.Item _item, Vector3 _pos)
    {
        theItemInfo.ShowInfo(_item, _pos);
    }

    public void HideInfo()
    {
        theItemInfo.HideInfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            ShowInfo(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideInfo();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        HideInfo();

        if (item != null)
            DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DragSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            Instantiate(DragSlot.instance.dragSlot.item,
                thePlayer.transform.position + thePlayer.transform.forward,
                Quaternion.identity);
            DragSlot.instance.dragSlot.ClearSlot();

        }

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item.Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }
}