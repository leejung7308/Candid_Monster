using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnlineMarket : MonoBehaviour
{
    [SerializeField]
    private GameObject go_OnlineMarketBase;

    [SerializeField]
    private TextMeshProUGUI text_Coin;

    private Inventory theInventory;

    [SerializeField]
    private TextMeshProUGUI txt_ItemName;
    [SerializeField]
    private TextMeshProUGUI txt_ItemDesc;

    [SerializeField]
    private OnBuyCount theOnBuyCount;

    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        Coin();
    }

    public void OpenOnlineMarket()
    {
        go_OnlineMarketBase.SetActive(true);
    }

    public void CloseOnlineMarket()
    {
        go_OnlineMarketBase.SetActive(false);

        txt_ItemName.text = "";
        txt_ItemDesc.text = "";

        theOnBuyCount.SetBuyCount();
    }

    private void Coin()
    {
        if (theInventory != null)
        {
            text_Coin.text = theInventory.GetCoinText();
        }
    }
}
