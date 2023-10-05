using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISmartphone : MonoBehaviour, IPointerClickHandler
{
    public static bool SmartphoneActivated = false;

    [SerializeField]
    private GameObject SmartphoneBase;

    // Start is called before the first frame update
    void Start()
    {

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
            if (SmartphoneActivated == false)
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
            if (SmartphoneActivated == false)
                OpenSmartphone();
            else
                CloseSmartphone();

        }
    }
 
    private void OpenSmartphone()
    {
        SmartphoneBase.SetActive(true);
        SmartphoneActivated = true;
    }

    private void CloseSmartphone()
    {
        SmartphoneBase.SetActive(false);
        SmartphoneActivated = false;
    }
}
