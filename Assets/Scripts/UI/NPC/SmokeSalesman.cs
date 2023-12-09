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

    void Update()
    {
        TryCloseSmokeStore();
    }

    private void TryCloseSmokeStore()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_SmokeStoreBase.activeSelf == true)
                CloseSmokeStore();
        }
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

    public bool CheckSmokeStore()
    {
        return go_SmokeStoreBase.activeSelf;
    }
}
