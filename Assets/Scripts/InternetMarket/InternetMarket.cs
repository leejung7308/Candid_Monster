using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InternetMarket : MonoBehaviour
{
    public static bool internetmarketActivated = false;  // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.

    [SerializeField]
    private GameObject go_InternetMarketBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 
    [SerializeField]
    private TextMeshProUGUI text_Coin;

    private IMItemInfo theIMItemInfo;
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        theIMItemInfo = FindObjectOfType<IMItemInfo>();
        theInventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInternetMarket();
        Coin();
    }

    private void TryOpenInternetMarket()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            internetmarketActivated = !internetmarketActivated;

            if (internetmarketActivated)
                OpenInternetMarket();
            else
                CloseInternetMarket();

        }
    }

    private void OpenInternetMarket()
    {
        go_InternetMarketBase.SetActive(true);
    }

    private void CloseInternetMarket()
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
