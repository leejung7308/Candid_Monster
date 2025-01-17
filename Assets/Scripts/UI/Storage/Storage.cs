using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    //[SerializeField] private Item.Item[] items;

    [SerializeField]
    private GameObject go_Inventory;
    [SerializeField]
    private GameObject go_SlotsParent;

    private StorageSlot[] theSlot;
    private ItemInfo theItemInfo;
    private Inventory theInventory;
    public Item.Item[] items;

    void Start()
    {
        theSlot = go_SlotsParent.GetComponentsInChildren<StorageSlot>();
        theItemInfo = FindObjectOfType<ItemInfo>();
        theInventory = FindObjectOfType<Inventory>();
        items = FindObjectOfType<Collection>().GetItems();
    }

    public StorageSlot[] GetStorageSlots() { return theSlot; }

    public void LoadToStorage(int _arrayNum, int _itemCode, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].itemCode == _itemCode)
                theSlot[_arrayNum].AddItem(items[i], _itemNum);
    }

    public void AcquireItem(Item.Item _item, int _count = 1)
    {
        if (Item.ItemCategory.Equipment != _item.itemCategory)
        {
            for (int i = 0; i < theSlot.Length; i++)
            {
                if (theSlot[i].item != null)
                {
                    if (theSlot[i].item.itemName == _item.itemName)
                    {
                        theSlot[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }
        if (Item.ItemCategory.ETC != _item.itemCategory)
        {
            for (int i = 0; i < theSlot.Length; i++)
            {
                if (theSlot[i].item == null)
                {
                    theSlot[i].AddItem(_item, _count);
                    return;
                }
            }
        }
    }

    public bool GetInventoryOpen()
    {
        if(go_Inventory.activeSelf == true)
        {
            return true;
        }else
            return false;
    }
}
