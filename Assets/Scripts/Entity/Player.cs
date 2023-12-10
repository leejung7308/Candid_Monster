using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityStatus
{
    public Player(float fatigue, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine, float maxFatigue, float maxAlcohol, float maxCaffeine, float maxNicotine) : 
        base(fatigue, moveSpeed, attackSpeed, alcohol, caffeine, nicotine, maxFatigue, maxAlcohol, maxCaffeine, maxNicotine) { }
    public float invincibleTime;
    public List<GameObject> weaponPrefabs;
    public List<GameObject> weapons;
    public GameObject weaponSpawnPos;
    public bool isMelee = true;
    public Inventory theInventory;
    private GameObject weapon;
    private float nextAttack;
    private Animator animator;

    Camera mainCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        for (int i = 0; i < 4; i++)
        {
            GameObject tmp = Instantiate(weaponPrefabs[i]);
            tmp.transform.position = new Vector2(1000, 1000);
            tmp.SetActive(false);
            tmp.transform.parent = transform.GetChild(0);
            weapons.Add(tmp);
        }
        SetWeapon(0);
    }
    private void Update()
    {
        if (isConfused)
        {
            weapon.tag = "Weapon(ConfusedPlayer)";
        }
        else
        {
            weapon.tag = "Weapon(Player)";
        }
        LookAt();
        WeaponSwap();
        ApplyDebuff(CheckDebuffCondition());
        if (!isFainted) Attack();
        if(fatigue>=100) EntityDie();
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
                animator.SetBool("isMove", true);
            }
        }
    }
    void LookAt()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("무기1번");
            SetWeapon(0);
            isMelee = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("무기2번");
            SetWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("무기3번");
            SetWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("무기4번");
            SetWeapon(3);
        }
        weapon.transform.position = weaponSpawnPos.transform.position;
        weapon.transform.rotation = weaponSpawnPos.transform.rotation;
    }
    void SetWeapon(int weaponNum)
    {
        weapons[weaponNum].gameObject.SetActive(true);
        for(int i = 0; i < 4; i++)
        {
            if (i != weaponNum)
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
        weapon = weapons[weaponNum];
    }
    void Attack()
    {
        if (Input.GetMouseButton(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackSpeed;
            animator.SetBool("isAttack", true);
        }
    }
    void AttackEnd()
    {
        animator.SetBool("isAttack", false);
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
        if ((collision.tag == "Weapon(Monster)" || collision.tag == "Weapon(ConfusedMonster)")&& !isInvincible)
        {
            Debug.Log("플레이어 피격");
            Item.DamageHolder currentDamageHolder = collision.GetComponent<Item.Weapon>().GetDamageHolder();
            EntityHit(currentDamageHolder);
            StartCoroutine(InvincibleMode(invincibleTime));
        }
        if(collision.tag == "Monster")
        {
            Item.DamageHolder currentDamageHolder = collision.GetComponent<Monster>().weapon.GetComponent<Item.Weapon>().GetDamageHolder();
        }
    }

    public void EquipItem(string _name)
    {
            if (_name == "knife")
            {
                weapons[0].gameObject.SetActive(true);
                weapons[1].gameObject.SetActive(false);
                weapons[2].gameObject.SetActive(false);
                weapons[3].gameObject.SetActive(false);
                weapon = weapons[0];
            }
            else if(_name == "alcohol")
            {
                weapons[1].gameObject.SetActive(true);
                weapons[0].gameObject.SetActive(false);
                weapons[2].gameObject.SetActive(false);
                weapons[3].gameObject.SetActive(false);
                weapon = weapons[1];
            }
            else if(_name == "coffee")
            {
                weapons[2].gameObject.SetActive(true);
                weapons[0].gameObject.SetActive(false);
                weapons[1].gameObject.SetActive(false);
                weapons[3].gameObject.SetActive(false);
                weapon = weapons[2];
            }
            else if(_name == "cigarette")
            {
                weapons[3].gameObject.SetActive(true);
                weapons[0].gameObject.SetActive(false);
                weapons[1].gameObject.SetActive(false);
                weapons[2].gameObject.SetActive(false);
                weapon = weapons[3];
            }
    }
}
