using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConversion : MonoBehaviour
{
    public GameObject StaticCamera;
    public GameObject MainCamera;
    public GameObject Village;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ConversionCamera();
    }

    private void ConversionCamera()
    {
        if (Village.activeSelf == false)
        {
            StaticCamera.SetActive(true);
            MainCamera.SetActive(false);
        }
        else
        {
            MainCamera.SetActive(true);
            StaticCamera.SetActive(false);
        }
    }
}
