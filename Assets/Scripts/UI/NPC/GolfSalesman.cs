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
        GameObject.Find("NPC_Smoke").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CloseGolfStore()
    {
        GameObject.Find("NPC_Smoke").transform.GetChild(0).gameObject.SetActive(true);
        go_GolfStoreBase.SetActive(false);
        theInventory.CloseInventory();

    }
}
