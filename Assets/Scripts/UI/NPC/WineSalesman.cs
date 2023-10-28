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
        GameObject.Find("NPC_Golf").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("NPC_Smoke").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("NPC_Convience").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CloseWineStore()
    {
        GameObject.Find("NPC_Golf").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("NPC_Smoke").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("NPC_Convience").transform.GetChild(0).gameObject.SetActive(true);
        go_WineStoreBase.SetActive(false);
        theInventory.CloseInventory();
    }
}
