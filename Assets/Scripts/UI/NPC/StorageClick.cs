using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_Storage;

    private Inventory theInventory;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_Storage.activeSelf == false)
        {
            OpenStorage();
            theInventory.OpenInventory();
        }
        else
        {
            CloseStorage();
            theInventory.CloseInventory();
        }
    }

    public void OpenStorage()
    {
        go_Storage.SetActive(true);
    }

    public void CloseStorage()
    {
        go_Storage.SetActive(false);
        theInventory.CloseInventory();
    }

    public bool CheckStorage()
    {
        return go_Storage.activeSelf;
    }
}
