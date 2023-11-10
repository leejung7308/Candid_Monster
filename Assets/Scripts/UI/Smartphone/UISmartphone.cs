using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISmartphone : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject SmartphoneBase;

    private Collection theCollection;
    private InternetMarket theInternetMarket;

    public GameObject IconCollection;
    public GameObject IconOnlineStore;
    public GameObject IconSetting;

    void Start()
    {
        theCollection = FindObjectOfType<Collection>();
        theInternetMarket = FindObjectOfType<InternetMarket>();
    }

    void Update()
    {
        TryOpenSmartphone();
        TryCloseSmartphone();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1)
        {
            if (SmartphoneBase.activeSelf == false)
            {
                OpenSmartphone();
            }
            else
            {
                CloseSmartphone();
            }
        }
    }

    private void TryOpenSmartphone()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (SmartphoneBase.activeSelf == false)
                OpenSmartphone();
            else
                CloseSmartphone();

        }
    }

    private void TryCloseSmartphone()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IconSetting.activeSelf==true)
        {
            if (SmartphoneBase.activeSelf == true)
                CloseSmartphone();
        }
    }
   
    public void OpenSmartphone()
    {
        SmartphoneBase.SetActive(true);
    }

    private void CloseSmartphone()
    {
        SmartphoneBase.SetActive(false);
    }
}
