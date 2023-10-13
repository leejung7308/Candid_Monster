using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickX : MonoBehaviour
{
    private Salesman theSalesman;
    // Start is called before the first frame update
    void Start()
    {
        theSalesman = FindObjectOfType<Salesman>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickQuitButton()
    {
        theSalesman.CloseOfflineStore();
    }
}
