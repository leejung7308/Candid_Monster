using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPierce : MonoBehaviour
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
        
    }

    public void Attack()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0);
        gameObject.SetActive(true);
        transform.localScale = new Vector3(1.2f, 0.1f, 0);
        gameObject.SetActive(false);
    }

    public int GetDamage()
    {
        return damage;
    }
}
