using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enchant : MonoBehaviour
{
    [SerializeField] private GameObject interfaces;
    [SerializeField] private Button enchantButton;
    Inventory inventory;
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
        weaponSlot.item.itemType = enchantItemSlot.item.itemType;
        weaponSlot.item.AddData(enchantItemSlot.item.GetData());
        switch (enchantItemSlot.item.itemType)
        {
            case Item.ItemType.alcohol:
                weaponSlot.item.GetComponent<SpriteRenderer>().sprite = alcohol;
                weaponSlot.item.itemImage = alcohol;
                break;
            case Item.ItemType.caffeine:
                weaponSlot.item.GetComponent<SpriteRenderer>().sprite = caffeine;
                weaponSlot.item.itemImage = caffeine;
                break;
            case Item.ItemType.nicotine:
                weaponSlot.item.GetComponent<SpriteRenderer>().sprite = nicotine;
                weaponSlot.item.itemImage = nicotine;
                break;
            default:
                break;
        }
        weaponSlot.item.itemName += " +1";
        weaponSlot.item.Enchant();
        resultSlot.AddItem(weaponSlot.item);
        weaponSlot.ClearSlot();
        enchantItemSlot.ClearSlot();
    }
}
