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

    // Start is called before the first frame update
    void Start()
    {
        theCollection = FindObjectOfType<Collection>();
        theInternetMarket = FindObjectOfType<InternetMarket>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenSmartphone();
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
   
    public void OpenSmartphone()
    {
        SmartphoneBase.SetActive(true);
    }

    private void CloseSmartphone()
    {
        GameObject.Find("Collection").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Home").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("OnlineStore").transform.GetChild(0).gameObject.SetActive(false);

        SmartphoneBase.SetActive(false);
    }
}
