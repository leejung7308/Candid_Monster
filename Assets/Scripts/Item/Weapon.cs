using UnityEngine;

namespace Item
{
    public class Weapon: Item
    {
        public float damage;
        public bool isEnchanted = false;
        public Weapon(int caffeine, int alcohol, int nicotine, float damage): base(caffeine, alcohol, nicotine)
        {
            this.damage = damage;
        }
        public override void Use()
        {
            Equipment equipment = FindObjectOfType<Equipment>();
            equipment.EquipItem(this);
        }
        public override float GetData() {  return damage; }
        public void CopyData(Item item)
        {
            this.alcohol = item.alcohol;
            this.caffeine = item.caffeine;
            this.nicotine = item.nicotine;
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
            this.alcohol = 0;
            this.caffeine = 0; 
            this.nicotine = 0;
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
                caffeine,
                alcohol,
                nicotine,
                damage
            );
    }
}