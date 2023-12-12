using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorSave : MonoBehaviour
{
    [SerializeField]
    private SaveAndLoad theSaveAndLoad;

    void Start()
    {
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();
    }
}
