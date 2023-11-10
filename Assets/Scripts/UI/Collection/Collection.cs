using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Collection : MonoBehaviour
{
    [SerializeField]
    private GameObject go_CollectionBase;

    public GameObject descHideImage;
    public GameObject IconOnlineMarket;
    public GameObject IconSetting;

    void Update()
    {
        TryCloseCollection();
    }

    private void TryCloseCollection()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (go_CollectionBase.activeSelf == true)
                CloseCollection();
        }
    }

    public void OpenCollection()
    {
        go_CollectionBase.SetActive(true);
        descHideImage.SetActive(true);
        IconSetting.SetActive(false);
        IconOnlineMarket.SetActive(false);
    }

    public void CloseCollection()
    {
        go_CollectionBase.SetActive(false);
        IconOnlineMarket.SetActive(true);
        IconSetting.SetActive(true);
    }
}
