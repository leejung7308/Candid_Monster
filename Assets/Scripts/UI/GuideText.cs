using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideText : MonoBehaviour
{
    public List<GameObject> texts = new List<GameObject>();
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player.transform.position.y < 6) SetText(0);
        if(player.transform.position.y>6 && player.transform.position.y < 27) SetText(1);
        if(player.transform.position.y>27) SetText(2);
    }
    void SetText(int index)
    {
        for(int i = 0; i < 3; i++)
        {
            if (i == index) texts[i].SetActive(true);
            else texts[i].SetActive(false);
        }
    }
}
