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
}
