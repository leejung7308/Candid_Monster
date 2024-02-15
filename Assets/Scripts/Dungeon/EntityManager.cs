using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    public int monsterCount;

    public List<InteractSniff> sniffs;

    // Update is called once per frame
    void Update()
    {
        if(monsterCount == 0)
        {
            for(int i = 0; i < doors.Count; i++)
            {
                doors[i].SetActive(false);
            }
        }
    }
}
