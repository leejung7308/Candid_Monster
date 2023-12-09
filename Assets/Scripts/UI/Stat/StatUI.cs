using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    EntityStatus player;
    public Image fatigueGauge;
    public Image alcoholGauge;
    public Image nicotineGauge;
    public Image caffeineGauge;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStatus>();
    }

    void Update()
    {
        fatigueGauge.fillAmount = player.fatigue / 100;
        alcoholGauge.fillAmount = player.alcohol / player.maxAlcohol;
        nicotineGauge.fillAmount = player.nicotine /player.maxNicotine;
        caffeineGauge.fillAmount = player.caffeine / player.maxCaffeine;
    }
}
