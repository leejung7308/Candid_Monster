using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnlineStore : MonoBehaviour
{
    private InternetMarket theInternetMarket;

    void Start()
    {
        theInternetMarket = FindObjectOfType<InternetMarket>();
    }

    private void OnClickOnlineStoreButton()
    {
        theInternetMarket.OpenInternetMarket();
        GameObject.Find("Icon_Setting").transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnClickQuitButton()
    {
        theInternetMarket.CloseInternetMarket();
        GameObject.Find("Icon_Setting").transform.GetChild(0).gameObject.SetActive(true);
    }
}
