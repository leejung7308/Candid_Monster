using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secretary : Monster
{
    public Secretary(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue) :
        base(fatigue, moveSpeed, attackSpeed, maxFatigue)
    { }
    protected override void EntityDie()
    {
        gameObject.GetComponent<DropTable>().ItemDrop(transform.position);
        room.GetComponent<BossRoom>().secretaryCount--;
        base.EntityDie();
    }
}
