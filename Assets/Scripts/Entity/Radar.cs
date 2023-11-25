using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    Monster monster;
    private void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!transform.parent.GetComponent<EntityStatus>().isConfused)
        {
            if(collision.tag == "ConfusedMonster" || collision.tag == "Player") 
            {
                monster.MonsterMovement(collision.gameObject);
                monster.LookAt(collision.gameObject);
            }
        }
        else if(transform.parent.GetComponent<EntityStatus>().isConfused || collision.tag == "ConfusedMonster")
        {
            if(collision.tag == "Monster" || collision.tag == "ConfusedMonster")
            {
                monster.MonsterMovement(collision.gameObject);
                monster.LookAt(collision.gameObject);
            }
        }
    }
}
