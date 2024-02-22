using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReplayButtonClick()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
