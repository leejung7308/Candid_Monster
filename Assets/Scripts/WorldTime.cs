using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldTime : MonoBehaviour
{
    private float currentTime;
    public TMP_Text timeText;
    public Image lightImage;
    public Color day;
    public Color evening;
    public Color night;
    private bool isSleep = false;
    void Start()
    {
        ResetWorldTime();
    }
    void Update()
    {
        if (!isSleep) UpdateWorldTime();
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
        currentTime += Time.deltaTime*60;
        float hour = currentTime / 60f;
        float minute = currentTime % 60f;
        timeText.text = string.Format("Time {0:D2} : {1:D2}", (int)hour, (int)minute);
        if ((int)hour == 24)
        {
            ResetWorldTime();
            isSleep = true;
            if(isSleep) StartCoroutine(Sleep());
        }
        if (hour >= 8 && hour <= 18)
        {
            lightImage.color = Color.Lerp(day, evening, (hour - 8) / 10);
        }
        else if(hour >= 18 && hour <= 20)
        {
            lightImage.color = Color.Lerp(evening, night, (hour - 18) / 2);
        }
    }
    public IEnumerator Sleep()
    {
        timeText.gameObject.SetActive(false);
        float fade = 0;
        Color originalColor = lightImage.color;
        for (int i = 0; i < 2; i++)
        {
            fade = 0;
            for (int j = 0; j < 100; j++)
            {
                fade += 0.01f;
                lightImage.color = Color.Lerp(originalColor, new Color(0, 0, 0, 1), fade);
                yield return new WaitForSeconds(0.005f);
            }
            fade = 0;
            for (int j = 0; j < 100; j++)
            {
                fade += 0.01f;
                lightImage.color = Color.Lerp(new Color(0, 0, 0, 1), originalColor, fade);
                yield return new WaitForSeconds(0.005f);
            }
        }
        fade = 0;
        for (int j = 0; j < 100; j++)
        {
            fade += 0.01f;
            lightImage.color = Color.Lerp(originalColor, new Color(0, 0, 0, 1), fade);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        fade = 0;
        for (int j = 0; j < 100; j++)
        {
            fade += 0.01f;
            lightImage.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0,0,0,0), fade); ;
            yield return new WaitForSeconds(0.01f);
        }
        timeText.gameObject.SetActive(true);
        isSleep = false;
    }
}
