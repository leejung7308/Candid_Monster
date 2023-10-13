using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCollection : MonoBehaviour
{
    private Collection theCollection;

    public GameObject OnlineStore;

    void Start()
    {
        theCollection = FindObjectOfType<Collection>();
    }

    private void OnClickCollectionButton()
    {
        theCollection.OpenCollection();
        OnlineStore.SetActive(false);
        GameObject.Find("Icon_Setting").transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnClickQuitButton()
    {
        OnlineStore.SetActive(true);
        theCollection.CloseCollection();
        GameObject.Find("Icon_Setting").transform.GetChild(0).gameObject.SetActive(true);
    }
}
