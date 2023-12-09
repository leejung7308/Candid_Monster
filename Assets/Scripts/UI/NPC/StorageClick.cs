using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_Storage;

    private Inventory theInventory;
    [SerializeField] 
    private SaveAndLoadStorage theSaveAndLoadStorage;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
        theSaveAndLoadStorage = FindObjectOfType<SaveAndLoadStorage>();
    }

    void Update()
    {
        TryCloseStorage();
    }

    private void TryCloseStorage()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_Storage.activeSelf == true)
                CloseStorage();
        }
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
        theSaveAndLoadStorage.LoadStorage();
        go_Storage.SetActive(true);
    }

    public void CloseStorage()
    {
        theSaveAndLoadStorage.ResetSaveStorage();
        theSaveAndLoadStorage.SaveStorage();
        go_Storage.SetActive(false);
        theInventory.CloseInventory();
    }
}
