using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType
{

}
public class Monster : EntityStatus
{
    public Monster(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue) : 
        base(fatigue, moveSpeed, attackSpeed, maxFatigue) { }
    public float invincibleTime;
    public GameObject attackRangeObject;
    public GameObject room;
    public bool isAlcohol;
    public bool isCaffeine;
    public bool isNicotine;
    NavMeshAgent agent;
    GameObject player;
    float nextAttack;
    public void Start()
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
       
        hitRange.tag = "Weapon(Monster)";
        gameObject.tag = "Monster";
        //SetWeapon();
        if (fatigue >= maxFatigue)
        {
            fatigue = -1;
            EntityDie();
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
    }

    public void HandleMonsterHit(Item.DamageHolder originalDamageHolder)
    {
        EntityHit(originalDamageHolder);
        StartCoroutine(InvincibleMode(invincibleTime));
    }

    public override DamageHolder GetDamageHolder() => weapon.GetComponent<Weapon>().GetDamageHolder();
}