using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Collection : MonoBehaviour
{
    [SerializeField]
    private GameObject go_CollectionBase;

    public GameObject descHideImage;
    public GameObject IconOnlineMarket;
    public GameObject IconSetting;

    private CollectionSlot[] equipmentslots;
    private CollectionSlot[] consumableslots;
    [SerializeField]
    private GameObject go_equipmentSlot;
    [SerializeField]
    private GameObject go_ConsumableSlot;
    public Inventory theInventory;
    public Item.Item[] items;

    void Start()
    {
        equipmentslots = go_equipmentSlot.GetComponentsInChildren<CollectionSlot>();
        consumableslots = go_ConsumableSlot.GetComponentsInChildren<CollectionSlot>();
        AcquireItem();
    }

    void Update()
    {
        TryCloseCollection();
    }

    public void AcquireItem()
    {
        int j = 0;
        for (int i = 0; i < items.Length; i++)  
        {
            if (items[i].itemCategory == Item.ItemCategory.Equipment)
            {
                equipmentslots[i].AddItem(items[i]);
            }
            else
            {
                consumableslots[j].AddItem(items[i]);
                j++;
            }
        }
    }
    public Item.Item[] GetItems() { return items; }

    public CollectionSlot[] GetEquipmentSlots() { return equipmentslots; }

    public CollectionSlot[] GetConsumableSlots() { return consumableslots; }

    private void TryCloseCollection()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_CollectionBase.activeSelf == true)
                CloseCollection();
        }
    }

    public void UnlockCollection(int itemCode)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(itemCode == items[i].itemCode) items[i].itemAcquire = 1; 
        }
        AcquireItem();
    }

    public void OpenCollection()
    {
        go_CollectionBase.SetActive(true);
        descHideImage.SetActive(true);
        IconSetting.SetActive(false);
        IconOnlineMarket.SetActive(false);
    }

    public void CloseCollection()
    {
        go_CollectionBase.SetActive(false);
        IconOnlineMarket.SetActive(true);
        IconSetting.SetActive(true);
    }
}
