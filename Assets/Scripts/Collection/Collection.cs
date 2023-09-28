using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Collection : MonoBehaviour
{
    public static bool collectionActivated = false;

    [SerializeField]
    private GameObject go_CollectionBase;

    public GameObject descHideImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        TryOpenCollection();
    }

    private void TryOpenCollection()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            collectionActivated = !collectionActivated;

            if (collectionActivated)
                OpenCollection();
            else
                CloseCollection();
        }
    }

    private void OpenCollection()
    {
        go_CollectionBase.SetActive(true);
    }

    private void CloseCollection()
    {
        go_CollectionBase.SetActive(false);
        descHideImage.SetActive(true);
    }
}
