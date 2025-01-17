using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WineSalesman : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_WineStoreBase;

    private Inventory theInventory;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        TryCloseWineStore();
    }

    private void TryCloseWineStore()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_WineStoreBase.activeSelf == true)
                CloseWineStore();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_WineStoreBase.activeSelf == false)
        {
            OpenWineStore();
            theInventory.OpenInventory();
        }
        else
        {
            //CloseWineStore();
        }
    }

    private void OpenWineStore()
    {
        go_WineStoreBase.SetActive(true);
    }

    public void CloseWineStore()
    {
        go_WineStoreBase.SetActive(false);
        theInventory.CloseInventory();
    }

    public bool CheckWineStore()
    {
        return go_WineStoreBase.activeSelf;
    }
}
