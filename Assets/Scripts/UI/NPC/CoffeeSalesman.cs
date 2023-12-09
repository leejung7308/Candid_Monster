using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoffeeSalesman : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_CoffeeStoreBase;

    private Inventory theInventory;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        TryCloseOfflineMarket();
    }

    private void TryCloseOfflineMarket()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_CoffeeStoreBase.activeSelf == true)
                CloseCoffeeStore();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_CoffeeStoreBase.activeSelf == false)
        {
            OpenCoffeeStore();
            theInventory.OpenInventory();
        }
        else
        {
            //CloseCoffeeStore();
        }
    }

    public void OpenCoffeeStore()
    {
        go_CoffeeStoreBase.SetActive(true);
    }

    public void CloseCoffeeStore()
    {
        go_CoffeeStoreBase.SetActive(false);
        theInventory.CloseInventory();

    }

    public bool CheckCoffeeStore()
    {
        return go_CoffeeStoreBase.activeSelf;
    }
}
