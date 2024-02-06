using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!transform.parent.GetComponent<EntityStatus>().isConfused)
        {
            if (collision.tag == "ConfusedMonster" || collision.tag == "Player")
            {
                transform.parent.GetComponent<Monster>().Attack();
            }
        }
        else if (transform.parent.GetComponent<EntityStatus>().isConfused)
        {
            if (collision.tag == "Monster" || collision.tag == "ConfusedMonster")
            {
                transform.parent.GetComponent<Monster>().Attack();
            }
        }
    }
}
