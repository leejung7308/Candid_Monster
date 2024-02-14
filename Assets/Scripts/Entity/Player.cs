using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;
using Entity.Skill;
using Item;

public class Player : EntityStatus
{
    public Player(float fatigue, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine, float maxFatigue, float maxAlcohol, float maxCaffeine, float maxNicotine) : 
        base(fatigue, moveSpeed, attackSpeed, alcohol, caffeine, nicotine, maxFatigue, maxAlcohol, maxCaffeine, maxNicotine) { }
    public float invincibleTime;
    public GameObject[] weapons;
    public GameObject weaponSpawnPos;
    public bool enableFatigue = true;
    float fatigueTimer = 0.0f;
    public Inventory theInventory;
    private Animator animator;
    float nextAttack;
    Dictionary<KeyCode, ActiveSkill> activeSkills;
    List<DamagePassive> damagePassives;
    List<PlayserStatusPassive> playerStatPassives;
    [SerializeField] float scale;
    [SerializeField] GameObject basicWeapon;

    Camera mainCamera;

    void Start()
    {
        hitRange.tag = "Weapon(Player)";
        animator = transform.GetChild(0).GetComponent<Animator>();
        mainCamera = Camera.main;
        weapons = new GameObject[4];
        weapons[0] = basicWeapon;
        SetWeapon(0);
        Debug.Log("Add Active skills");
        activeSkills = new Dictionary<KeyCode, ActiveSkill>();
        activeSkills.Add(KeyCode.E, new ThrowAlcoholBottle(this));
        activeSkills.Add(KeyCode.R, new LetsGoDinner(this));
        activeSkills.Add(KeyCode.T, new EspressoDoubleShot(this));
        activeSkills.Add(KeyCode.Y, new ElectronicSmoking(this));
        activeSkills.Add(KeyCode.U, new OneByOneSmoking(this));
        Debug.Log("Add Damage Passives");
        damagePassives = new List<DamagePassive>();
        damagePassives.Add(new BombAlcohol());
        Debug.Log("Add Player Status Passives");
        /*playerStatPassives = new List<PlayserStatusPassive>();
        playerStatPassives.Add(new CoffeBoost(this));
        playerStatPassives.Add(new SmokingTime(this));*/
    }
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (isConfused) hitRange.tag = "Weapon(ConfusedPlayer)";
        else hitRange.tag = "Weapon(Player)";

        WeaponSwap();
        //ApplyPlayerStatusPassives();
        //HandleActiveSkills();
        Attack();
        LookAt();
        if (enableFatigue) IncreaseFatigue();
        if(fatigue>=maxFatigue) EntityDie();
        ApplyDebuff(CheckDebuffCondition());
    }
    void FixedUpdate()
    {
        if (!isFainted)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) animator.SetBool("isMove", false);
            else
            {
                float xinput = Input.GetAxis("Horizontal");
                float yinput = Input.GetAxis("Vertical");
                Vector2 newVelocity = new Vector2(xinput, yinput);
                gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity * moveSpeed;
                transform.GetChild(0).GetComponent<AnimationManager>().MoveStart();
            }
        }
        else transform.GetChild(0).GetComponent<AnimationManager>().MoveEnd();
    }
    void LookAt()
    {
        if (isFainted)
            return;

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(scale, scale, 1);
        }
        else
        {
            transform.localScale = new Vector3(-scale, scale, 1);
        }
    }
    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weapons[0] == null) return;
            Debug.Log("무기1번");
            SetWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons[1] == null) return;
            Debug.Log("무기2번");
            SetWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weapons[2] == null) return;
            Debug.Log("무기3번");
            SetWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (weapons[3] == null) return;
            Debug.Log("무기4번");
            SetWeapon(3);
        }
    }
    void SetWeapon(int weaponNum)
    {
        weapons[weaponNum].gameObject.SetActive(true);
        for(int i = 0; i < 4; i++)
        {
            if (weapons[i] != null && i != weaponNum) 
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
        weapon = weapons[weaponNum];
        weapon.transform.position = weaponSpawnPos.transform.position;
    }
    /**
     * 무기의 DamageHolder에 플레이어에게 적용중인 패시브 스킬로 인한 데미지 변화를 적용한다.
     */
    DamageHolder ApplyDamagePassives(DamageHolder original)
    {
        DamageHolder dh = original;
        foreach (DamagePassive dp in damagePassives)
        {
            dh = dp.ApplyDamageChange(dh);
        }
        return dh;
    }

    void ApplyPlayerStatusPassives()
    {
        foreach (PlayserStatusPassive psp in playerStatPassives)
        {
            psp.ApplyStatusChange();
        }
    }
    void HandleActiveSkills()
    {
        foreach (KeyValuePair<KeyCode, ActiveSkill> pair in activeSkills)
        {
            pair.Value.HandleCooldown();
            if(Input.GetKeyDown(pair.Key))
            {
                Debug.Log($"Apply ActiveSkill `{pair.Value.GetType().Name}`");
                pair.Value.Activate();
            }
        }
    }
    void Attack()
    {
        if (isFainted)
            return;

        if (Input.GetMouseButton(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            transform.GetChild(0).GetComponent<AnimationManager>().AttackStart();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item.Item hitObject = collision.gameObject.GetComponent<Item.Item>();

            if (hitObject != null)
            {
                theInventory.AcquireItem(hitObject);
                collision.gameObject.SetActive(false);
            }
        }
        if (!isInvincible && (collision.CompareTag("Weapon(Monster)") || collision.CompareTag("Weapon(ConfusedMonster)") || collision.CompareTag("Monster")))
        {
            Debug.Log("플레이어 피격");
            HandleEntityDamage(collision.GetComponentInParent<Monster>().GetDamageHolder());
        }
    }
    
    /**
     * 플레이어의 현제 공격으로 인한 DamageHolder 객체를 생성한다.
     * 무기의 DamageHolder를 기본으로 하고, 여러 DamagePassive들로 인한 데미지 변화를 반영한 최종 결과를 반환한다.
     */
    public override DamageHolder GetDamageHolder()
    {
        DamageHolder dh = weapon.GetComponent<Weapon>().GetDamageHolder();
        dh = ApplyDamagePassives(dh);
        return dh;
    }
    
    /**
     * 플레이어의 데미지 처리를 담당한다.
     * @param originalDamageHolder: 플레이어에게 가해질 데미지.
     */
    public void HandleEntityDamage(DamageHolder originalDamageHolder)
    {
        EntityHit(originalDamageHolder);
        StartCoroutine(InvincibleMode(invincibleTime));     // 중복해서 피해를 입는것을 방지하기 위한 일시 무적.
    }

    public void EquipItem(Item.Item item,int index)
    {
        weapons[index] = item.gameObject;
    }
    
    /**
     * 피로도 증가 로직.
     */
    void IncreaseFatigue()
    {
        fatigueTimer += Time.deltaTime;
        if(fatigueTimer >= 6.0f) // 6초에 한 번씩 피로도 증가.
        {
            Debug.Log("Increase Fatigue!");
            fatigue += 1;
            fatigueTimer = 0.0f;
        }
    }
}
