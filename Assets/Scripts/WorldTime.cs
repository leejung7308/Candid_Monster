using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldTime : MonoBehaviour
{
    private float currentTime;
    public TMP_Text timeText;
    void Start()
    {
        ResetWorldTime();
    }
    void Update()
    {
        UpdateWorldTime();
    }
    public void ResetWorldTime()
    {
        currentTime = 480f;
    }
    public float GetCurrentTime()
    {
        return currentTime;
    }
    private void UpdateWorldTime()
    {
        currentTime += Time.deltaTime;
        float hour = GetCurrentTime() / 60f;
        float minute = GetCurrentTime() % 60f;
        timeText.text = string.Format("Time {0:D2} : {1:D2}", (int)hour, (int)minute);
        if (hour >= 24f)
        {
            ResetWorldTime();
        }
    }
}
