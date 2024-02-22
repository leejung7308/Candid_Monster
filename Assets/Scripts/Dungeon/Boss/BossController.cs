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

    //보스 이동시 딜레이
    private float moveDelay;

    //플레이어와의 거리
    private float distance;

    //일정 타수 이상 맞으면 이동 위한 카운트
    public int avoidMoveCount;

    public float invincibleTime;

    //5회 공격시 피해증가 카운트
    public int enhancedBossAttack;
    public bool isEnhanced;


    public BossController(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue) 
        : base(fatigue, moveSpeed, attackSpeed, maxFatigue) { }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enhancedBossAttack = 0;
        coolTime = Time.time + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fatigue >= maxFatigue)
        {
            fatigue = -1;
            EntityDie();
        }

        //보스와 플레이어 사이의 거리
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        BossMovement();
        LookAt(player);

        if (Time.time > coolTime)
        {
            ExcuteBossPattern(distance);
            coolTime = coolTime + 10f;
        }

        if(enhancedBossAttack > 3)
        {
            enhancedBossAttack = 0;
        }
    }

    protected override void EntityDie()
    {
        gameObject.GetComponent<DropTable>().ItemDrop(transform.position);
        base.EntityDie();
    }

    public void BossMovement()
    {
        //조건에 따라 보스방 내부 임의 위치로 순간이동
        if( avoidMoveCount >= 3 && distance < 2.3 )
        {
            float randPosX = Random.Range(-94f, -78f);
            float randPosY = Random.Range(75f, 85f);
            while ( Vector3.Distance( player.transform.position, new Vector3(randPosX,randPosY,0) ) < 5 )
            {
                randPosX = Random.Range(-94f, -78f);
                randPosY = Random.Range(75f, 85f);
            }
            gameObject.transform.position = new Vector3(randPosX, randPosY, 0);
            avoidMoveCount = 0;
            Debug.Log("보스가 이동");
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
        bossAttacks[2].GetComponent<BossAttackPointingFlame>().Attack();
        Debug.Log("AttackPointingFlame");
        /*f (num > 4)
        {
            //Pointing Flame 공격
            bossAttacks[2].GetComponent<BossAttackPointingFlame>().Attack();
            Debug.Log("AttackPointingFlame");
            
        }
        else if(num > 2.5)
        {
            //BladeAura 공격
            bossAttacks[1].GetComponent<BossAttackBladeAura>().Attack();
            Debug.Log("AttackBladeAura");
        }
        else
        {
            //Pierce 공격
            bossAttacks[0].GetComponent<BossAttackPierce>().Attack();
            Debug.Log("AttackPierce");
        }*/
        enhancedBossAttack++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon(Player)"))
        {
            HandleBossHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 1f);
            avoidMoveCount++;
        }
    }

    public void HandleBossHit(Item.DamageHolder originalDamageHolder, float rate)
    {
        EntityHit(originalDamageHolder, rate);
        StartCoroutine(InvincibleMode(invincibleTime));
    }

    public override DamageHolder GetDamageHolder() => weapon.GetComponent<Weapon>().GetDamageHolder();
}