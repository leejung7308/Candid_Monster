using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyWeapon;
    [SerializeField]
    private GameObject slotsParent;
    Player player;
    Slot[] slots;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        slots =  slotsParent.GetComponentsInChildren<Slot>();
        for(int  i = 0; i<4; i++)
        {
            slots[i].tag = "EquipmentSlot";
            if (player.weapons[i] != null)
            {
                slots[i].AddItem(player.weapons[i].GetComponent<Item.Item>());
            }
        }
    }
    void Update()
    {
    }
    public void SetPlayerWeapon()
    {
        for (int i = 0; i < 4; i++)
        {
            if (slots[i] != null)
            {
                GameObject tmp = Instantiate(emptyWeapon);
                tmp.GetComponent<Weapon>().itemImage = slots[i].item.itemImage;
                tmp.GetComponent<Weapon>().damage = slots[i].GetComponent<Weapon>().damage;
                player.weapons[i] = tmp;
            }
            else
            {
                player.weapons[i] = null;
            }
        }
    }
}
