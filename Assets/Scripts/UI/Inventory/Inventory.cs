using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Inventory : MonoBehaviour
{
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
    private CollectionSlot theCollectionSlot;
    private CoffeeSalesman theCoffeeSalesman;
    private WineSalesman theWineSalesman;
    private GolfSalesman theGolfSalesman;
    private SmokeSalesman theSmokeSalesman;
    private StorageClick theStorageClick;



    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        theCollectionSlot = FindObjectOfType<CollectionSlot>();
        theCoffeeSalesman = FindObjectOfType<CoffeeSalesman>();
        theWineSalesman = FindObjectOfType<WineSalesman>();
        theGolfSalesman = FindObjectOfType<GolfSalesman>();
        theSmokeSalesman = FindObjectOfType<SmokeSalesman>();
        theStorageClick = FindObjectOfType<StorageClick>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (go_InventoryBase.activeSelf==false)
                OpenInventory();
            else
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
        //_targetTr.position = new Vector3(400, 200, 0); //인벤토리 오픈 위치 고정
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
        }else if (Item.ItemType.ETC == _item.itemType)
        {
            text_Coin.text = (_count * _item.itemValue).ToString();
        }
        if(Item.ItemType.ETC != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item, _count);
                    return;
                }
            }
        }
    }

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
        try
        {
            if (theCoffeeSalesman.CheckCoffeeStore() != null && theCoffeeSalesman.CheckCoffeeStore() == true)
            {
                return true;
            }
            else
                return false;
        }
        catch (NullReferenceException ie)
        {
            return false;
        }
    }
    public bool CheckWineStore()
    {
        try
        {
            if (theWineSalesman.CheckWineStore() != null && theWineSalesman.CheckWineStore() == true)
            {
                return true;
            }
            else
                return false;
        }
        catch (NullReferenceException ie)
        {
            return false;
        }
    }
    public bool CheckSmokeStore()
    {
        try
        {
            if (theSmokeSalesman.CheckSmokeStore() != null && theSmokeSalesman.CheckSmokeStore() == true)
            {
                return true;
            }
            else
                return false;
        }
        catch (NullReferenceException ie)
        {
            return false;
        }
    }
    public bool CheckGolfStore()
    {
        try
        {
            if (theGolfSalesman.CheckGolfStore() != null && theGolfSalesman.CheckGolfStore() == true)
            {
                return true;
            }
            else
                return false;
        }
        catch (NullReferenceException ie)
        {
            return false;
        }
    }

    public bool CheckStorage()
    {
        try
        {
            if (theStorageClick.CheckStorage() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (NullReferenceException ie)
        {
            return false;
        }
    }
}