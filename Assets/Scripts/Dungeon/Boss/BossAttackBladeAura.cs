using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossAttackBladeAura : MonoBehaviour
{
    public GameObject player;
    float activeTime;
    public GameObject boss;
    public int damage;
    public bool isAttack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {

        }
    }

    public void Attack()
    {
        gameObject.transform.position = boss.transform.position;
        if (boss.transform.localScale.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1.25f, 3f, 0);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1.25f, 3f, 0);
        }

        activeTime = Time.time + 4f;
        gameObject.SetActive(true);

        if (boss.transform.localScale.x > 0)
        {
            while (transform.position.x > -93f)
            {
                transform.position += new Vector3(-0.3f,0,0);
            }
        }
        else
        {
            while (transform.position.x < -78f)
            {
                transform.position += new Vector3(0.3f, 0, 0);
            }
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
