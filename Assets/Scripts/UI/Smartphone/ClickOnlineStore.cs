using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnlineStore : MonoBehaviour
{
    private InternetMarket theInternetMarket;

    // Start is called before the first frame update
    void Start()
    {
        theInternetMarket = FindObjectOfType<InternetMarket>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickOnlineStoreButton()
    {
        theInternetMarket.OpenInternetMarket();
    }

    public void OnClickQuitButton()
    {
        theInternetMarket.CloseInternetMarket();
    }
}
