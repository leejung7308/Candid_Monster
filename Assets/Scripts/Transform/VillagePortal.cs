using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagePortal : MonoBehaviour
{
    private Player thePlayer;
    public GameObject Grid_Village;
    public GameObject Grid_Coffee;
    public GameObject Grid_Golf;
    public GameObject Grid_Smoke;
    public GameObject Grid_Wine;
    public GameObject Grid_Convience;
    public GameObject Grid_Home;
    public GameObject HomePortal;
    public GameObject CoffeePortal;
    public GameObject GolfPortal;
    public GameObject SmokePortal;
    public GameObject WinePortal;
    public GameObject ConviencePortal;
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
            Grid_Village.SetActive(true);
            if (Grid_Home.activeSelf == true)
            {
                thePlayer.transform.position = HomePortal.transform.position + new Vector3(0, -3, 0);
            }else if (Grid_Wine.activeSelf == true)
            {
                thePlayer.transform.position = WinePortal.transform.position + new Vector3(0, -3, 0);
            }else if (Grid_Coffee.activeSelf == true)
            {
                thePlayer.transform.position = CoffeePortal.transform.position + new Vector3(0, -3, 0);
            }else if (Grid_Smoke.activeSelf == true)
            {
                thePlayer.transform.position = SmokePortal.transform.position + new Vector3(0, -3, 0);
            }else if (Grid_Convience.activeSelf == true)
            {
                thePlayer.transform.position = ConviencePortal.transform.position + new Vector3(0, -3, 0);
            }else if (Grid_Golf.activeSelf == true)
            {
                thePlayer.transform.position = GolfPortal.transform.position + new Vector3(0, -3, 0);
            }
            Grid_Home.SetActive(false);
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
            Computer.SetActive(false);
            Storage.SetActive(false);
        }
    }
}
