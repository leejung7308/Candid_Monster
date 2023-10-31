using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    GameObject player;
    public Image hpGauge;
    public Image alcoholGauge;
    public Image nicotineGauge;
    public Image caffeineGauge;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        hpGauge.fillAmount = player.GetComponent<EntityStatus>().hp / 10;
        alcoholGauge.fillAmount = player.GetComponent<EntityStatus>().alcohol / 100;
        nicotineGauge.fillAmount = player.GetComponent<EntityStatus>().nicotine / 100;
        caffeineGauge.fillAmount = player.GetComponent<EntityStatus>().caffeine / 100;
    }
}
