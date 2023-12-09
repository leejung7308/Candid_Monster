using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Smartphonehome : MonoBehaviour, IPointerClickHandler
{
    private UISmartphone theUISmartphone;

    void Start()
    {
        theUISmartphone = FindObjectOfType<UISmartphone>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            theUISmartphone.OpenSmartphone();
        }
    }
}
