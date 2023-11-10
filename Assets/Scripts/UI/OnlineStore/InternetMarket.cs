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
    private TextMeshProUGUI text_Coin;
    private Inventory theInventory;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    [SerializeField]
    private BuyCount theBuyCount;

    public GameObject IconCollection;
    public GameObject IconSetting;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        Coin();
        TryCloseInternetMarket();
    }

    public void TryCloseInternetMarket()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_InternetMarketBase.activeSelf == true)
                CloseInternetMarket();
        }
    }

    public void OpenInternetMarket()
    {
        go_InternetMarketBase.SetActive(true);
        IconSetting.SetActive(false);
        IconCollection.SetActive(false);
    }

    public void CloseInternetMarket()
    {
        go_InternetMarketBase.SetActive(false);

        txt_ItemName.text = "";
        txt_ItemDesc.text = "";

        theBuyCount.SetBuyCount();
        IconCollection.SetActive(true);
        IconSetting.SetActive(true);
    }

    private void Coin()
    {
        if (theInventory != null)
        {
            text_Coin.text = theInventory.GetCoinText();
        }
    }
}
