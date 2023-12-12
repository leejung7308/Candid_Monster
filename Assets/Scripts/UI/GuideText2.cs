using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideText2 : MonoBehaviour
{
    public GameObject text_room1;
    public GameObject text_room2;
    public GameObject text_room3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text_room1.SetActive(false);
            text_room2.SetActive(false);
            text_room3.SetActive(true);
        }
    }
}
