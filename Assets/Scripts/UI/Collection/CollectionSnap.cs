using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSnap : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Equip;
    [SerializeField]
    private GameObject go_Consumable;

    public void OpenEquipPage()
    {
        go_Equip.SetActive(true);
        go_Consumable.SetActive(false);
    }

    public void OpenConsumablePage()
    {
        go_Consumable.SetActive(true);
        go_Equip.SetActive(false);
    }
}
