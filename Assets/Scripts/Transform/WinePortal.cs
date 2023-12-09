using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinePortal : MonoBehaviour
{
    private Player thePlayer;
    public GameObject Grid_Village;
    public GameObject Grid_Coffee;
    public GameObject Grid_Golf;
    public GameObject Grid_Smoke;
    public GameObject Grid_Wine;
    public GameObject Grid_Convience;
    public GameObject Grid_Home;
    public GameObject VillagePortal;
    public GameObject NPC_Coffee;
    public GameObject NPC_Smoke;
    public GameObject NPC_Golf;
    public GameObject NPC_Wine;
    public GameObject NPC_Convience;
    public GameObject Computer;
    public GameObject Storage;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Grid_Wine.SetActive(true);
            NPC_Wine.SetActive(true);
            thePlayer.transform.position = VillagePortal.transform.position + new Vector3(0, 3, 0);
            Grid_Village.SetActive(false);
            Grid_Coffee.SetActive(false);
            Grid_Golf.SetActive(false);
            Grid_Smoke.SetActive(false);
            Grid_Home.SetActive(false);
            Grid_Convience.SetActive(false);
            NPC_Coffee.SetActive(false);
            NPC_Smoke.SetActive(false);
            NPC_Golf.SetActive(false);
            
            NPC_Convience.SetActive(false);
            Computer.SetActive(false);
            Storage.SetActive(false);
        }
    }
}
