using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

public class AoEProjectile : Projectile
{
    [SerializeField]
    GameObject aoeFieldPrefab;
    
    public override void Break(Collider2D other)
    {
        SpawnAoE();
        Destroy(gameObject);
    }
    
    /**
     * Area of Effect prefab을 소환한다.
     */
    void SpawnAoE()
    {
        Instantiate(aoeFieldPrefab, transform.position, Quaternion.identity);
    }
}
