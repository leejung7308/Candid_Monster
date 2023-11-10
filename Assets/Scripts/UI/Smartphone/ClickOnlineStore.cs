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
    }

    private void OnClickQuitButton()
    {
        theInternetMarket.CloseInternetMarket();
    }
}
