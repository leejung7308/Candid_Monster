using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Inventory : MonoBehaviour
{
    public Item.Item[] items;

    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Transform _targetTr;

    [SerializeField]
    private TextMeshProUGUI text_Coin;

    private Slot[] slots;

    private ItemInfo theItemInfo;
    public GameObject CoffeeStore;
    public GameObject WineStore;
    public GameObject GolfStore;
    public GameObject SmokeStore;
    public GameObject ConvienceStore;
    public GameObject Storage;
    public Collection theCollection;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        theItemInfo = FindObjectOfType<ItemInfo>();
    }

    void Update()
    {
        TryOpenInventory();
        TryCloseInventory();
    }

    public Slot[] GetSlots() { return slots; }

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].itemName == _itemName)
                slots[_arrayNum].AddItem(items[i], _itemNum);
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (go_InventoryBase.activeSelf == false)
                OpenInventory();
            else
                CloseInventory();

        }
    }

    private void TryCloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_InventoryBase.activeSelf == true)
                CloseInventory();
        }
    }

    public void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    public void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        theItemInfo.HideInfo();
        //_targetTr.position = new Vector3(400, 200, 0); //Inventory open position static
    }

    public void AcquireItem(Item.Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType && Item.ItemType.ETC != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        else if (Item.ItemType.ETC == _item.itemType)
        {
            int Money;
            Money = int.Parse(text_Coin.text);
            Money += _count * _item.itemValue;
            text_Coin.text = Money.ToString();
        }

        if (Item.ItemType.ETC != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
                    UnlockCollection(_item);
                    return;
                }
            }
        }
    }

    public void UnlockCollection(Item.Item _item)
    {
        Item.Item[] items = theCollection.GetItems();

        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].itemName == _item.itemName)
            {
                items[i].itemAcquire = 1;
            }
        }
    }

    public Item.Item[] GetItems() { return items; }

    public string GetCoinText()
    {
        return text_Coin.text;
    }

    public void SetCoinText(int _text_Coin)
    {
        int CoinText = int.Parse(text_Coin.text);
        CoinText -= _text_Coin;
        text_Coin.text = CoinText.ToString();
    }

    public bool CheckCoffeeStore()
    {
        if (CoffeeStore.activeSelf == true)
        {
            return true;
        }
        else return false;
    }
    public bool CheckWineStore()
    {
        if (WineStore.activeSelf == true)
        {
            return true;
        }
        else return false;
    }
    public bool CheckSmokeStore()
    {
        if (SmokeStore.activeSelf == true)
        {
            return true;
        }
        else return false;
    }
    public bool CheckGolfStore()
    {
        if (GolfStore.activeSelf == true)
        {
            return true;
        }
        else return false;
    }
    public bool CheckConvienceStore()
    {
        if (ConvienceStore.activeSelf == true)
        {
            return true;
        }
        else return false;
    }

    public bool CheckStorage()
    {
        if (Storage.activeSelf == true)
        {
            return true;
        }
        else return false;
    }
}