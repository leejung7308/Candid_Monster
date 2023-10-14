using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickX : MonoBehaviour
{
    private Salesman theSalesman;

    void Start()
    {
        theSalesman = FindObjectOfType<Salesman>();
    }

    public void OnClickQuitButton()
    {
        theSalesman.CloseOfflineStore();
    }
}
