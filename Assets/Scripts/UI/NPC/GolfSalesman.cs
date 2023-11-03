using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GolfSalesman : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_GolfStoreBase;

    private Inventory theInventory;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_GolfStoreBase.activeSelf == false)
        {
            OpenGolfStore();
            theInventory.OpenInventory();
        }
        else
        {
            //CloseGolfStore();
        }
    }

    public void OpenGolfStore()
    {
        go_GolfStoreBase.SetActive(true);
    }

    public void CloseGolfStore()
    {
        go_GolfStoreBase.SetActive(false);
        theInventory.CloseInventory();
    }

    public bool CheckGolfStore()
    {
        return go_GolfStoreBase.activeSelf;
    }
}
