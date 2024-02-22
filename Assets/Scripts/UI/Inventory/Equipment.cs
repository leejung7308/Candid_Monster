using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyWeapon;
    [SerializeField]
    private GameObject slotsParent;
    Player player;
    Inventory inventory;
    Slot[] slots;
    GameObject weaponSpawnPoint;
    Item.Item[] items;
    void Start()
    {
        items = FindObjectOfType<Collection>().GetItems();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weaponSpawnPoint = GameObject.FindGameObjectWithTag("WeaponSpawnPoint");
        inventory = FindObjectOfType<Inventory>();
        slots =  slotsParent.GetComponentsInChildren<Slot>();
        for(int  i = 0; i<4; i++)
        {
            slots[i].tag = "EquipmentSlot";
        }
    }
    public void SetPlayerWeapon()
    {
        for (int i = 0; i < 4; i++)
        {
            if (slots[i].item != null)
            {
                Debug.Log(slots[i].item.itemName);
                player.weapons[i].GetComponent<Weapon>().CopyData(slots[i].item);
                player.weapons[i].GetComponent<SpriteRenderer>().sprite = slots[i].itemImage.sprite;
            }
            else
            {
                player.weapons[i].GetComponent<Weapon>().Reset();
                player.weapons[i].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }
    public void EquipItem(Item.Item item)
    {
        Debug.Log(item.itemName);
        for (int i = 0; i < 4; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item);
                SetPlayerWeapon();
                return;
            }
        }
        Item.Item tmp = slots[0].item;
        slots[0].AddItem(item);
        inventory.AcquireItem(tmp);
        SetPlayerWeapon();
    }
    public Slot[] GetSlots() { return slots; }

    public void LoadToEquipment(int _arrayNum, int _itemCode)
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].itemCode == _itemCode)
                slots[_arrayNum].AddItem(items[i]);
    }
}
