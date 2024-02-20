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
    public int itemCount;
    public Image itemImage;
    [SerializeField]
    private GameObject go_CountImage;
    [SerializeField]
    private TextMeshProUGUI text_Count;

    private Player thePlayer;
    private ItemInfo theItemInfo;
    private Inventory theInventory;
    private Storage theStorage;
    private Equipment theEquipment;
    private Enchant theEnchant;

    private Rect baseRect; 

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        theInventory = FindObjectOfType<Inventory>();
        theStorage = FindObjectOfType<Storage>();
        theEquipment = FindObjectOfType<Equipment>();
        theEnchant = FindObjectOfType<Enchant>();
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
    }

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

        if (item.itemCategory != Item.ItemCategory.Equipment && item.itemCategory != Item.ItemCategory.ETC && item.itemCategory != Item.ItemCategory.EnchantItem)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else if(item.itemCategory == Item.ItemCategory.Equipment || item.itemCategory == Item.ItemCategory.EnchantItem) 
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

    public void ClearSlot()
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
                    theInventory.SetCoinText(-(item.itemValue / 2)); //sell item
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
                else if(theInventory.CheckEnchantTable() == true)
                {
                    if (item.itemCategory == Item.ItemCategory.Equipment && (this.CompareTag("InventorySlot") || this.CompareTag("EquipmentSlot")) && !item.IsEnchanted()) theEnchant.PutEquipment(item);
                    else if (item.itemCategory == Item.ItemCategory.EnchantItem && this.CompareTag("InventorySlot")) theEnchant.PutEnchantItem(item);
                    else if (this.CompareTag("Enchant(Weapon)") || this.CompareTag("Enchant(Item)") || this.CompareTag("Enchant(Result)")) theInventory.AcquireItem(item);
                    else return;
                    ClearSlot();
                }
                else 
                {
                    if (item.itemCategory == Item.ItemCategory.Equipment && !this.CompareTag("EquipmentSlot"))
                    {
                        item.Use();
                        ClearSlot();
                    }
                    else if (item.itemCategory == Item.ItemCategory.Equipment && this.CompareTag("EquipmentSlot"))
                    {
                        theInventory.AcquireItem(item);
                        ClearSlot();
                        theEquipment.SetPlayerWeapon();
                    }
                    else if (item.itemCategory == Item.ItemCategory.EnchantItem) return;
                    else
                    {
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
        /*if (DragSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            Debug.Log("이거 실행");
            *//*Instantiate(DragSlot.instance.dragSlot.item,
                thePlayer.transform.position + thePlayer.transform.forward,
                Quaternion.identity);*//*
            DragSlot.instance.dragSlot.ClearSlot();

        }*/

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot.item.itemCategory != Item.ItemCategory.Equipment && this.CompareTag("EquipmentSlot")) return;
            if (DragSlot.instance.dragSlot.item.itemCategory != Item.ItemCategory.Equipment && this.CompareTag("Enchant(Weapon)")) return;
            if (DragSlot.instance.dragSlot.item.itemCategory != Item.ItemCategory.EnchantItem && this.CompareTag("Enchant(Item)")) return;
            if (this.CompareTag("Enchant(Weapon)") && DragSlot.instance.dragSlot.item.IsEnchanted()) return;
            if (this.CompareTag("Enchant(Result)")) return;
            else ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        Item.Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        Debug.Log(item.itemName);
        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
        if (this.CompareTag("EquipmentSlot") || DragSlot.instance.dragSlot.CompareTag("EquipmentSlot"))
        {
            theEquipment.SetPlayerWeapon();
        }
    }
}