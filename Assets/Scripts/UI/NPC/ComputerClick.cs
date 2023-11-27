using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject go_Computer;

    public OnlineMarket theOnlineMarket;

    void Update()
    {
        TryCloseComputer();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (go_Computer.activeSelf == false)
        {
            OpenComputer();
            theOnlineMarket.OpenOnlineMarket();
        }
    }

    public void OpenComputer()
    {
        go_Computer.SetActive(true);
    }
    public void CloseComputer()
    {
        go_Computer.SetActive(false);
    }

    private void TryCloseComputer()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_Computer.activeSelf == true)
            {
                theOnlineMarket.CloseOnlineMarket();
                CloseComputer();

            }
        }
    }
}
