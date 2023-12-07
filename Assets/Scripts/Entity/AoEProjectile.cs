using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class AoEProjectile : MonoBehaviour
{
    [SerializeField]
    bool createAoE = false;
    [SerializeField]
    GameObject aoeFieldPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Monster") || other.CompareTag("Player") || other.CompareTag("ConfusedMonster"))
        {
            if (createAoE)
                SpawnAoE();
            Destroy(this);
        }
    }
    
    /**
     * Area of Effect prefab을 소환한다.
     */
    void SpawnAoE()
    {
        Instantiate(aoeFieldPrefab, transform.position, Quaternion.identity);
    }
}
