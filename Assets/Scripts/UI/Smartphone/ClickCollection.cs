using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCollection : MonoBehaviour
{
    private Collection theCollection;

    void Start()
    {
        theCollection = FindObjectOfType<Collection>();
    }

    private void OnClickCollectionButton()
    {
        theCollection.OpenCollection();
    }

    private void OnClickQuitButton()
    {
        theCollection.CloseCollection();
    }
}
