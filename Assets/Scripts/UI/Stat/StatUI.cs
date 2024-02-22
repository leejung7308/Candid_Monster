using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    EntityStatus player;
    public Image fatigueGauge;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStatus>();
    }

    void Update()
    {
        fatigueGauge.fillAmount = player.fatigue / 100;
    }
}
