using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarket : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InternetMarketBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private TextMeshProUGUI text_Coin;

    private IMItemInfo theIMItemInfo;
    private Inventory theInventory;

    void Start()
    {
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        Coin();
    }

    public void OpenInternetMarket()
    {
        go_InternetMarketBase.SetActive(true);
    }

    public void CloseInternetMarket()
    {
        go_InternetMarketBase.SetActive(false);
        theIMItemInfo.HideToolTip();
    }

    private void Coin()
    {
        if (theInventory != null)
        {
            text_Coin.text = theInventory.GetCoinText();
        }
    }
}
