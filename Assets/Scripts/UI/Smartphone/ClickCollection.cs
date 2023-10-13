using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCollection : MonoBehaviour
{
    private Collection theCollection;

    public GameObject OnlineStore;

    // Start is called before the first frame update
    void Start()
    {
        theCollection = FindObjectOfType<Collection>();
    }

    public void OnClickCollectionButton()
    {
        theCollection.OpenCollection();
        OnlineStore.SetActive(false);
        GameObject.Find("Icon_Setting").transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClickQuitButton()
    {
        OnlineStore.SetActive(true);
        theCollection.CloseCollection();
        GameObject.Find("Icon_Setting").transform.GetChild(0).gameObject.SetActive(true);
    }
}
