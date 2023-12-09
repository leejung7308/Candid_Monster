using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

public class Monster : EntityStatus
{
    public Monster(float fatigue, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine, float maxFatigue, float maxAlcohol, float maxCaffeine, float maxNicotine) : 
        base(fatigue, moveSpeed, attackSpeed, alcohol, caffeine, nicotine, maxFatigue, maxAlcohol, maxCaffeine, maxNicotine) { }
    public float invincibleTime;
    public GameObject radarObject;
    public GameObject attackRangeObject;
    public GameObject angle;
    public GameObject weaponPrefab;
    public GameObject weapon;
    public GameObject weaponSpawnPos;
    public Vector2 spawnPoint;
    public bool isMelee = true;
    Radar radar;
    Radar attackRange;
    GameObject player;
    float nextAttack;
    void Start()
    {
        radar = radarObject.GetComponent<Radar>();
        attackRange = attackRangeObject.GetComponent<Radar>();
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = Instantiate(weaponPrefab);
        weapon.transform.parent = transform;
        weapon.tag = "Weapon(Monster)";
    }
    void Update()
    {
        if (isConfused)
        {
            weapon.tag = "Weapon(ConfusedMonster)";
            gameObject.tag = "ConfusedMonster";
        }
        else
        {
            weapon.tag = "Weapon(Monster)";
            gameObject.tag = "Monster";
        }
        ApplyDebuff(CheckDebuffCondition());
        SetWeapon();
        if (fatigue>maxFatigue) EntityDie();
    }
    public void MonsterMovement(GameObject follow)
    {
        if(isFainted)
            return;
        
        transform.position = Vector2.MoveTowards(transform.position, follow.transform.position, Time.deltaTime * moveSpeed);
    }
    public void LookAt(GameObject follow)
    {
        if(isFainted)
            return;
        
        if (follow.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void Attack()
    {
        if(isFainted)
            return;
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            StartCoroutine(Swing(angle));
        }
    }
    void SetWeapon()
    {
        weapon.transform.position = weaponSpawnPos.transform.position;
        weapon.transform.rotation = weaponSpawnPos.transform.rotation;
    }
    protected override void EntityDie()
    {
        gameObject.GetComponent<DropTable>().ItemDrop(transform.position);
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