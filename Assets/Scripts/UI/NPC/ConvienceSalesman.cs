using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConvienceSalesman : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_ConvienceStoreBase;

    private Inventory theInventory;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_ConvienceStoreBase.activeSelf == false)
        {
            OpenConvienceStore();
            theInventory.OpenInventory();
        }
        else
        {
            //CloseConvienceStore();
        }
    }

    public void OpenConvienceStore()
    {
        go_ConvienceStoreBase.SetActive(true);
    }

    public void CloseConvienceStore()
    {
        go_ConvienceStoreBase.SetActive(false);
        theInventory.CloseInventory();
    }
}
