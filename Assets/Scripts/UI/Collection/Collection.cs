using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Collection : MonoBehaviour
{
    [SerializeField]
    private GameObject go_CollectionBase;

    public GameObject descHideImage;

    public void OpenCollection()
    {
        go_CollectionBase.SetActive(true);
    }

    public void CloseCollection()
    {
        go_CollectionBase.SetActive(false);
        descHideImage.SetActive(true);
    }
}
