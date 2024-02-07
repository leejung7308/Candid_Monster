using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.AI;

public class Monster : EntityStatus
{
    public Monster(float fatigue, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine, float maxFatigue, float maxAlcohol, float maxCaffeine, float maxNicotine) : 
        base(fatigue, moveSpeed, attackSpeed, alcohol, caffeine, nicotine, maxFatigue, maxAlcohol, maxCaffeine, maxNicotine) { }
    public float invincibleTime;
    public GameObject attackRangeObject;
    public GameObject room;
    public bool isAlcohol;
    public bool isCaffeine;
    public bool isNicotine;
    NavMeshAgent agent;
    GameObject player;
    float nextAttack;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        hitRange.tag = "Weapon(Monster)";
    }
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (isConfused)
        {
            hitRange.tag = "Weapon(ConfusedMonster)";
            gameObject.tag = "ConfusedMonster";
        }
        else
        {
            hitRange.tag = "Weapon(Monster)";
            gameObject.tag = "Monster";
        }
        ApplyDebuff(CheckDebuffCondition());
        //SetWeapon();
        if (fatigue >= maxFatigue)
        {
            fatigue = -1;
            EntityDie();
        }
    }
    public void MonsterMovement(GameObject follow)
    {
        if(isFainted)
            return;

        agent.SetDestination(follow.transform.position);
        transform.GetChild(0).GetComponent<AnimationManager>().MoveStart();
        //transform.position = Vector2.MoveTowards(transform.position, follow.transform.position, Time.deltaTime * moveSpeed);
    }
    public void LookAt(GameObject follow)
    {
        if(isFainted)
            return;
        
        if (follow.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
    }
    public void Attack()
    {
        if(isFainted)
            return;
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            transform.GetChild(0).GetComponent<AnimationManager>().AttackStart();
        }
    }
    /*void SetWeapon()
    {
        weapon.transform.position = transform.position + new Vector3(0,1,0);
        weapon.transform.rotation = transform.rotation;
    }*/
    protected override void EntityDie()
    {
        gameObject.GetComponent<DropTable>().ItemDrop(transform.position);
        room.GetComponent<EntityManager>().monsterCount--;
        base.EntityDie();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible)       // 무적일 경우, 충돌 연산을 무시한다.
            return;

        if(collision.CompareTag("Weapon(Player)"))
        {
            Debug.Log("몬스터가 플레이어와 충돌.");
            HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder());
        }
        else if(isConfused && (collision.CompareTag("Weapon(Monster)") || collision.CompareTag("Weapon(ConfusedMonster)")))
        {
            Debug.Log("몬스터가 혼란 상태이고, 몬스터와 충돌.");
            HandleMonsterHit(collision.GetComponentInParent<Monster>().GetDamageHolder());
        }
        else if (collision.CompareTag("Weapon(ConfusedMonster)"))
        {
            Debug.Log("몬스터가 혼란 상태인 몬스터에게 공격당함.");
            HandleMonsterHit(collision.GetComponentInParent<Monster>().GetDamageHolder());
        }
    }

    public void HandleMonsterHit(Item.DamageHolder originalDamageHolder)
    {
        EntityHit(originalDamageHolder);
        StartCoroutine(InvincibleMode(invincibleTime));
    }

    public override DamageHolder GetDamageHolder() => weapon.GetComponent<Weapon>().GetDamageHolder();
}