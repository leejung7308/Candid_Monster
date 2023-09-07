using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject radarObject;
    public GameObject attackRangeObject;
    public GameObject angle;
    public GameObject weaponPrefab;
    public GameObject weapon;
    public GameObject weaponSpawnPos;
    public float moveSpeed = 5f;
    public float weaponSpeed = 5f;
    public float attackCoolDown = 0.5f;
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
        if (isMelee)
            weapon.tag = "Melee Weapon(Monster)";
        else
        {
            weapon.tag = "Ranged Weapon(Monster)";
            weapon.GetComponent<Collider2D>().enabled = false;
        }

    }
    private void Update()
    {
        SetWeapon();
        Attack();
        LookAt();
        MonsterMovement();
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
            nextAttack = Time.time + attackCoolDown;
            StartCoroutine(Swing());
        }
    }
    private void Shoot()
    {
        if (!isMelee)
        {
            Vector2 shootingDir = (Vector2)player.transform.position - (Vector2)weaponSpawnPos.transform.position;
            shootingDir.Normalize();
            GameObject shootingObject = Instantiate(weapon);
            shootingObject.GetComponent<Collider2D>().enabled = true;
            shootingObject.transform.position = weaponSpawnPos.transform.position;
            shootingObject.transform.up = shootingDir;
            shootingObject.GetComponent<Rigidbody2D>().velocity = shootingDir * weaponSpeed;
        }
    }
    private void SetWeapon()
    {
        weapon.transform.position = weaponSpawnPos.transform.position;
        weapon.transform.rotation = weaponSpawnPos.transform.rotation;
    }
    private IEnumerator Swing()
    {
        for (int i = 90; i >= -90; i--)
        {
            angle.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, i);
            if (i == 0) Shoot();
            yield return new WaitForSeconds(0.001f);
        }
        angle.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ranged Weapon(Player)")
        {
            Debug.Log("몬스터 피격(원거리)");
            Destroy(col.gameObject);
        }
        else if (col.tag == "Melee Weapon(Player)")
        {
            Debug.Log("몬스터 피격(근거리)");
        }
    }
}