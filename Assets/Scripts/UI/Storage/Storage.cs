using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Inventory;
    [SerializeField]
    private GameObject go_SlotsParent;
    private StorageSlot[] theSlot;
    private ItemInfo theItemInfo;

    void Start()
    {
        theSlot = go_SlotsParent.GetComponentsInChildren<StorageSlot>();
        theItemInfo = FindObjectOfType<ItemInfo>();
    }

    public void AcquireItem(Item.Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
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
        if (Item.ItemType.ETC != _item.itemType)
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
