using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Salesman : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_OfflineStoreBase;


    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_OfflineStoreBase.activeSelf==false)
        {
            OpenOfflineStore();
        }
        else
        {
            CloseOfflineStore();
        }
    }

    public void OpenOfflineStore()
    {
        go_OfflineStoreBase.SetActive(true);
    }

    public void CloseOfflineStore()
    {
        go_OfflineStoreBase.SetActive(false);

    }
}
