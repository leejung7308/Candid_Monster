using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportPosition;
    public GameObject nextRoom;
    
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            nextRoom.SetActive(true);
            collision.transform.position = TeleportPosition.transform.position;
            transform.parent.transform.gameObject.SetActive(false);
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            nextRoom.SetActive(true);
            collision.transform.position = TeleportPosition.transform.position;
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
