using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraConversion : MonoBehaviour
{
    public GameObject ExitCamera;
    public GameObject MainCamera;
    public GameObject Exit;
    public GameObject TutorialClear;
    public GameObject room3text;
    public GameObject room3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExitCamera.SetActive(true);
            MainCamera.SetActive(false);
            Exit.SetActive(true);
            TutorialClear.SetActive(true);
            room3.GetComponent<GuideText>().enabled = false;
            room3text.SetActive(false);
        }
    }
}
