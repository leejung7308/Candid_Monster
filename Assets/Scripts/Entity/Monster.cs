using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        radar = radarObject.GetComponent<Radar>();
        attackRange = attackRangeObject.GetComponent<Radar>();
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = Instantiate(weaponPrefab);
        weapon.transform.parent = transform;
        weapon.tag = "Weapon(Monster)";
    }
    private void Update()
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
        if (fatigue>100) EntityDie();
    }
    public void MonsterMovement(GameObject follow)
    {
        if (!isFainted)
        {
            transform.position = Vector2.MoveTowards(transform.position, follow.transform.position, Time.deltaTime * moveSpeed);
        }        
    }
    public void LookAt(GameObject follow)
    {
        if (!isFainted)
        {
            if (follow.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    public void Attack()
    {
        if (!isFainted)
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackSpeed;
                StartCoroutine(Swing(angle));
            }
        }
    }
    private void SetWeapon()
    {
        weapon.transform.position = weaponSpawnPos.transform.position;
        weapon.transform.rotation = weaponSpawnPos.transform.rotation;
    }
    protected override void EntityDie()
    {
        gameObject.GetComponent<DropTable>().ItemDrop(transform.position);
        base.EntityDie();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvincible)
        {
            if (!isConfused && (collision.CompareTag("Weapon(Player)") || collision.CompareTag("Weapon(ConfusedMonster)")))
            {
                Debug.Log("몬스터 피격");
                HandleMonsterHit(collision.GetComponent<Player>().GetDamageHolder());
            }
            else if (collision.CompareTag("Weapon(Player)") || collision.CompareTag("Weapon(Monster)") ||
                     collision.CompareTag("Weapon(ConfusedMonster)"))
            {
                Debug.Log("몬스터 피격");
                HandleMonsterHit(collision.GetComponent<Player>().GetDamageHolder());
            }
        }
    }

    public void HandleMonsterHit(Item.DamageHolder originalDamageHolder)
    {
        EntityHit(originalDamageHolder);
        StartCoroutine(InvincibleMode(invincibleTime));
    }
}