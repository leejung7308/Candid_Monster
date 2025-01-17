using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingDeongeonManager : MonoBehaviour
{
    public string sceneName = "FirstFloor";
    public Slider slider;
    private AsyncOperation operation;

    private SaveAndLoad theSaveAndLoad;

    public static LoadingDeongeonManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(operation.progress, 1f, timer);
                if (slider.value >= operation.progress)
                    timer = 0f;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, timer);
                if (slider.value >= 0.99f)
                    operation.allowSceneActivation = true;
            }
        }

        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();
        gameObject.SetActive(false);
    }
}
