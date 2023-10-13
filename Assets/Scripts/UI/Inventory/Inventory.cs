using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        theCollectionSlot = FindObjectOfType<CollectionSlot>();
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
    
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        theItemInfo.HideToolTip();
        //_targetTr.position = new Vector3(400, 200, 0);
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
}