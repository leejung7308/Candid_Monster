using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePortal : MonoBehaviour
{
    public static HomePortal instance;
    private SaveAndLoad theSaveAndLoad;

    void Start()
    {
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        StartCoroutine(LoadCoroutine());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            theSaveAndLoad.ResetSaveData();
            theSaveAndLoad.SaveData();
            StartCoroutine(LoadCoroutine());
        }
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("HomeScene");

        while (!operation.isDone)
        {
            yield return null;
        }

        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();
    }

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
}
