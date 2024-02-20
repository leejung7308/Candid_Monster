using UnityEngine;

namespace Item
{
    public class Weapon: Item
    {
        public int damage;
        public bool isEnchanted = false;
        public Weapon(int damage)
        {
            this.damage = damage;
        }
        public override void Use()
        {
            Equipment equipment = FindObjectOfType<Equipment>();
            equipment.EquipItem(this);
        }
        public override int GetData() {  return damage; }
        public override bool IsEnchanted() { return isEnchanted; }
        public override void AddData(int data)
        {
            damage += data;
        }
        public override void Enchant()
        {
            isEnchanted = true;
        }
        public void CopyData(Item item)
        {
            this.itemCategory = item.itemCategory;
            this.itemValue = item.itemValue;
            this.itemDesc = item.itemDesc;
            this.itemType = item.itemType;
            this.itemAcquire = item.itemAcquire;
            this.itemName = item.itemName;
            this.itemImage = item.itemImage;
            this.damage = item.GetData();
        }
        public void Reset()
        {
            this.itemCategory = 0;
            this.itemValue = 0;
            this.itemDesc = "";
            this.itemAcquire = 0;
            this.itemType = 0;
            this.itemName = "";
            this.itemImage = null;
            this.damage = 0;
        }
        public override DamageHolder GetDamageHolder()
            => new DamageHolder(
                damage
            );
    }
}