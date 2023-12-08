using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject lockObject;
    public int secretaryCount;

    // Update is called once per frame
    void Update()
    {
        if (secretaryCount == 0)
        {
            lockObject.SetActive(false);
        }
    }
}
