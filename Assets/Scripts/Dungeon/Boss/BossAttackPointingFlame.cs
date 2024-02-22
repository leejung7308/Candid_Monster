using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPointingFlame : MonoBehaviour
{
    public GameObject player;
    float activeTime;
    public GameObject boss;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > activeTime)
        {
            gameObject.SetActive(false);
            transform.position = boss.transform.position;
        }
    }

    public void Attack()
    {
        transform.position = player.transform.position;

        activeTime = Time.time + 3f;
        gameObject.SetActive(true);
    }

    public int GetDamage()
    {
        return damage;
    }

}
