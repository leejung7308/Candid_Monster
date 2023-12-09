using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secretary : Monster
{
    public Secretary(float fatigue, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine, float maxFatigue, float maxAlcohol, float maxCaffeine, float maxNicotine) :
        base(fatigue, moveSpeed, attackSpeed, alcohol, caffeine, nicotine, maxFatigue, maxAlcohol, maxCaffeine, maxNicotine)
    { }
    protected override void EntityDie()
    {
        gameObject.GetComponent<DropTable>().ItemDrop(transform.position);
        room.GetComponent<BossRoom>().secretaryCount--;
        base.EntityDie();
    }
}
