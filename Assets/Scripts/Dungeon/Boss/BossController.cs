using Item;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BossController : EntityStatus
{
    public List<GameObject> bossAttacks;
    public GameObject BossField;
    private GameObject player;
    private NavMeshAgent agent;

    //보스 공격패턴 실행을 위한 쿨타임
    private float coolTime;

    //플레이어와의 거리
    private float distance;

    //일정 타수 이상 맞으면 이동 위한 카운트
    public int avoidMoveCount;

    public float invincibleTime;

    //3 or 5회 공격시 피해증가 카운트
    public int enhancedBossAttack;


    public BossController(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue) 
        : base(fatigue, moveSpeed, attackSpeed, maxFatigue) { }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        enhancedBossAttack = 0;
        coolTime = 10;




    }

    // Update is called once per frame
    void Update()
    {
        //보스와 플레이어 사이의 거리
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        BossMovement();

        if (Time.time > coolTime)
        {
            ExcuteBossPattern((distance / 20) * 2);
        }
    }

    public void BossMovement()
    {
        //조건에 따라 보스방 내부 임의 위치로 순간이동
        if( /*avoidMoveCount >= 3 &&*/ distance < 0.5 )
        {
            float randPosX = Random.Range(-8.5f, 8.5f);
            float randPosY = Random.Range(-9f, 1f);

            gameObject.transform.position = new Vector3(randPosX, randPosY, 0);
        }
    }

    public void LookAt(GameObject follow)
    {
        if (follow.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
    }

    private void ExcuteBossPattern(float num)
    {
        if(num > 1.3)
        {
            //Pointing Flame 공격
            //bossAttacks[2];
            
        }
        else if(num > 0.3)
        {
            //BladeAura 공격
            //bossAttatcks[1];
        }
        else
        {
            //Pierce 공격
            //bossAttacks[0];
        }
        enhancedBossAttack++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon(Player)"))
        {

        }
        else 
        {
            
        }
    }

    public void HandleBossHit(Item.DamageHolder originalDamageHolder, float rate)
    {
        EntityHit(originalDamageHolder, rate);
        StartCoroutine(InvincibleMode(invincibleTime));
    }

    public override DamageHolder GetDamageHolder() => weapon.GetComponent<Weapon>().GetDamageHolder();
}