using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmokeSalesman : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_SmokeStoreBase;

    private Inventory theInventory;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_SmokeStoreBase.activeSelf == false)
        {
            OpenSmokeStore();
            theInventory.OpenInventory();
        }
        else
        {
            //CloseSmokeStore();
        }
    }

    public void OpenSmokeStore()
    {
        go_SmokeStoreBase.SetActive(true);
        GameObject.Find("NPC_Convience").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CloseSmokeStore()
    {
        GameObject.Find("NPC_Convience").transform.GetChild(0).gameObject.SetActive(true);
        go_SmokeStoreBase.SetActive(false);
        theInventory.CloseInventory();

    }
}
