using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public Item.Item item;
        [Range(0,100)]
        public int percentage;
    }
    public List<DropItem> dropItemTable = new List<DropItem>();
    protected List<Item.Item> dropItems = new List<Item.Item>();
    protected void Gacha() 
    {
        for(int i = 0; i < dropItemTable.Count; i++)
        {
            int rnd = Random.Range(0, 100);
            if(rnd < dropItemTable[i].percentage)
            {
                dropItems.Add(dropItemTable[i].item);
            }
        }
    }
    public void ItemDrop(Vector2 pos)
    {
        Gacha();
        foreach(var item in dropItems)
        {
            Instantiate(item, pos, Quaternion.identity);
        }
    }
}