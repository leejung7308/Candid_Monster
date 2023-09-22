using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : EntityStatus
{
    public Monster(float hp, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine) : base(hp, moveSpeed, attackSpeed, alcohol, caffeine, nicotine) { }
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
        SetWeapon();
        Attack();
        LookAt();
        MonsterMovement();
        EntityDie();
    }
    private void MonsterMovement()
    {
        if (radar.isDetected) //플레이어가 레이더에 감지 되면 플레이어를 따라감
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        }
        else //플레이어가 레이더를 벗어나면 원래 위치(스폰 위치)로 돌아감
        {
            transform.position = Vector2.MoveTowards(transform.position, spawnPoint, Time.deltaTime * moveSpeed);
        }
    }
    private void LookAt()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Attack()
    {
        if (attackRange.isDetected && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            StartCoroutine(Swing(angle));
        }
    }
    private void SetWeapon()
    {
        weapon.transform.position = weaponSpawnPos.transform.position;
        weapon.transform.rotation = weaponSpawnPos.transform.rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon(Player)" && !isInvincible)
        {
            Debug.Log("몬스터 피격"); 
            Item.DamageHolder currentDamageHolder = collision.GetComponent<Item.Weapon>().GetDamageHolder();
            EntityHit(currentDamageHolder);
            StartCoroutine(InvincibleMode(invincibleTime));
        }
    }
}