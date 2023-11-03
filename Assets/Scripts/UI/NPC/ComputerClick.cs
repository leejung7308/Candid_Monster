using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_OnlineMarket;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_OnlineMarket.activeSelf == false)
        {
            OpenOnlineMarket();
        }
        else
        {
            CloseOnlineMarket();
        }
    }

    public void OpenOnlineMarket()
    {
        go_OnlineMarket.SetActive(true);
    }

    public void CloseOnlineMarket()
    {
        go_OnlineMarket.SetActive(false);
    }
}
