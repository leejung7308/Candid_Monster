using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum MonsterType
{
    None,
    Alcohol, 
    Caffeine,
    Nicotine,
}

public class Monster : EntityStatus
{
    public Monster(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue) : 
        base(fatigue, moveSpeed, attackSpeed, maxFatigue) { }
    public float invincibleTime;
    public GameObject attackRangeObject;
    public GameObject room;

    /*
    public bool isAlcohol;
    public bool isCaffeine;
    public bool isNicotine;
    */
    public MonsterType monsterType;

    NavMeshAgent agent;
    GameObject player;
    float nextAttack;
    public GameObject auraObject;


    public bool isBerserk = false;
    public bool isWeakness = false;

    float berserkSpeed;
    float berserkAttackSpeed;

    float originSpeed;
    float originAttackSpeed;

    float weaknessSpeed;
    float weaknessAttackSpeed;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        hitRange.tag = "Weapon(Monster)";
        auraObject.SetActive(false);

        MonsterConditionCheck();
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
       
        hitRange.tag = "Weapon(Monster)";
        gameObject.tag = "Monster";
        //SetWeapon();
        if (fatigue >= maxFatigue)
        {
            fatigue = -1;
            EntityDie();
        }

        auraObject.transform.position = transform.position + new Vector3(0, 0.6f, 0);
         
        //Berserk Condition Check
        if (isBerserk)
        {
            agent.speed = berserkSpeed;
            base.attackSpeed = berserkAttackSpeed;
            auraObject.SetActive(true);
        }
        else
        {
            agent.speed = originSpeed;
            base.attackSpeed = originAttackSpeed;
            auraObject.SetActive(false);
        }

        //Weakness Condtion Check
        if (isWeakness)
        {
            agent.speed = weaknessSpeed;
            base.attackSpeed = weaknessAttackSpeed;
        }
        else
        {
            agent.speed = originSpeed;
            base.attackSpeed = originAttackSpeed;
        }




    }
    public void MonsterMovement(GameObject follow)
    {
        agent.SetDestination(follow.transform.position);
        transform.GetChild(0).GetComponent<AnimationManager>().MoveStart();
        //transform.position = Vector2.MoveTowards(transform.position, follow.transform.position, Time.deltaTime * moveSpeed);
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
    public void Attack()
    {
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
        isBerserk = false;
        isWeakness = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible)       // 무적일 경우, 충돌 연산을 무시한다.
            return;
        
        //When Collision with Weapon Collider
        if(collision.CompareTag("Weapon(Player)"))
        {
            if (isBerserk || isWeakness)
            {
                if (collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType == ItemType.None)
                {
                    Debug.Log("ConditionTrue 몬스터가 NoneType 충돌.");
                    HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 1);
                }
                if (collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType == ItemType.Alcohol)
                {
                    Debug.Log("ConditionTrue 몬스터가 AlcoholType 충돌.");
                    HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 1);
                }
                if (collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType == ItemType.Nicotine)
                {
                    Debug.Log("ConditionTrue 몬스터가 NicotineType 충돌.");
                    HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 1);
                }
                if (collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType == ItemType.Caffeine)
                {
                    Debug.Log("ConditionTrue 몬스터가 CaffeineType 충돌.");
                    HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 1);
                }
            }
            else
            {
                if (collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType == ItemType.None)
                {
                    Debug.Log("ConditionFalse 몬스터가 x1 공격당함.");
                    HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 1);
                }
                else
                {
                    if (collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType.ToString().Equals(monsterType.ToString()))
                    {
                        isWeakness = true;
                        Debug.Log("몬스터가 약점에 공격당함.");
                        HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 2);
                    }
                    if (!collision.GetComponentInParent<Player>().weapon.GetComponent<Weapon>().itemType.ToString().Equals(monsterType.ToString()))
                    {
                        isBerserk = true;
                        Debug.Log("몬스터가 광폭함.");
                        HandleMonsterHit(collision.GetComponentInParent<Player>().GetDamageHolder(), 0.5f);
                    }
                }
            }
        }
    }

    public void HandleMonsterHit(Item.DamageHolder originalDamageHolder, float rate)
    {
        EntityHit(originalDamageHolder, rate);
        StartCoroutine(InvincibleMode(invincibleTime));
    }

    public override DamageHolder GetDamageHolder() => weapon.GetComponent<Weapon>().GetDamageHolder();

    public void MonsterConditionCheck()
    {
        originSpeed = agent.speed;
        originAttackSpeed = attackSpeed;

        berserkSpeed = agent.speed * 2f;
        berserkAttackSpeed = attackSpeed * 2f;

        weaknessSpeed = agent.speed / 2f;
        weaknessAttackSpeed = attackSpeed / 2f;
    }
}