using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus : MonoBehaviour
{
    public float hp;
    public float moveSpeed;
    public float attackSpeed;
    public float alcohol;
    public float caffeine;
    public float nicotine;
    public EntityStatus(float hp, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.alcohol = alcohol;
        this.caffeine = caffeine;
        this.nicotine = nicotine;
    }
}
