using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTutorial : MonoBehaviour
{
    [SerializeField]
    private SaveAndLoad theSaveAndLoad;

    void start()
    {
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            theSaveAndLoad.ResetSaveData();
            theSaveAndLoad.SaveData();
            SceneManager.LoadScene("LoadingFirstFloor");
        }
    }
}
