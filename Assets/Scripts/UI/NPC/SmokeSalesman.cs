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
    }

    public void CloseSmokeStore()
    {
        go_SmokeStoreBase.SetActive(false);
        theInventory.CloseInventory();

    }
}
