using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePortal : MonoBehaviour
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
    public GameObject EnchantTable;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Grid_Home.SetActive(true);
            Computer.SetActive(true);
            Storage.SetActive(true);
            EnchantTable.SetActive(true);
            thePlayer.transform.position = VillagePortal.transform.position + new Vector3(0, 3, 0);
            Grid_Village.SetActive(false);
            Grid_Coffee.SetActive(false);
            Grid_Golf.SetActive(false);
            Grid_Smoke.SetActive(false);
            Grid_Wine.SetActive(false);
            Grid_Convience.SetActive(false);
            NPC_Coffee.SetActive(false);
            NPC_Smoke.SetActive(false);
            NPC_Golf.SetActive(false);
            NPC_Wine.SetActive(false);
            NPC_Convience.SetActive(false);
        }
    }
}
