using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantItem : Item.Item
{
    public int additionalDamage;
    Enchant enchant;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        enchant = FindObjectOfType<Enchant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Use()
    {
        enchant.PutEnchantItem(this);
    }
    public override int GetData()
    {
        return additionalDamage;
    }
    public override bool IsEnchanted()
    {
        return false;
    }
    public override void AddData(int data)
    {
        return;
    }
    public override void Enchant()
    {
        return;
    }
}
