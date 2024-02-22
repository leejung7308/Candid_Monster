using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enchant : MonoBehaviour
{
    [SerializeField] private GameObject interfaces;
    [SerializeField] private Button enchantButton;
    Inventory inventory;
    Collection collection;
    public Slot weaponSlot;
    public Slot enchantItemSlot;
    public Slot resultSlot;
    public Sprite alcohol;
    public Sprite caffeine;
    public Sprite nicotine;
    void Start()
    {
        weaponSlot.tag = "Enchant(Weapon)";
        enchantItemSlot.tag = "Enchant(Item)";
        resultSlot.tag = "Enchant(Result)";
        inventory = FindObjectOfType<Inventory>();
        collection = FindObjectOfType<Collection>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseEnchantTable();
        }
        if (weaponSlot.item != null && enchantItemSlot.item != null) enchantButton.gameObject.SetActive(true);
        else enchantButton.gameObject.SetActive(false);
        
    }
    public void OpenEnchantTable()
    {
        interfaces.SetActive(true);
        inventory.OpenInventory();
    }
    public void CloseEnchantTable() 
    { 
        interfaces.SetActive(false);
        inventory.CloseInventory();
    }
    public void PutEnchantItem(Item.Item enchantItem)
    {
        enchantItemSlot.AddItem(enchantItem);
    }
    public void PutEquipment(Item.Item equipment)
    {
        weaponSlot.AddItem(equipment);
    }
    public void EnchantWeapon()
    {
        for(int i = 0; i < collection.items.Length; i++)
        {
            if (weaponSlot.item.itemCode + enchantItemSlot.item.itemCode % 1000 == collection.items[i].itemCode)
            {
                resultSlot.AddItem(collection.items[i]);
            }
        }
        /*weaponSlot.item.itemType = enchantItemSlot.item.itemType;
        weaponSlot.item.AddData(enchantItemSlot.item.GetData());
        switch (enchantItemSlot.item.itemType)
        {
            case Item.ItemType.Alcohol:
                weaponSlot.item.GetComponent<SpriteRenderer>().sprite = alcohol;
                weaponSlot.item.itemImage = alcohol;
                break;
            case Item.ItemType.Caffeine:
                weaponSlot.item.GetComponent<SpriteRenderer>().sprite = caffeine;
                weaponSlot.item.itemImage = caffeine;
                break;
            case Item.ItemType.Nicotine:
                weaponSlot.item.GetComponent<SpriteRenderer>().sprite = nicotine;
                weaponSlot.item.itemImage = nicotine;
                break;
            default:
                break;
        }
        weaponSlot.item.itemName += " +1";*/
        resultSlot.item.Enchant();
        //resultSlot.AddItem(weaponSlot.item);
        weaponSlot.ClearSlot();
        enchantItemSlot.ClearSlot();
    }
}
