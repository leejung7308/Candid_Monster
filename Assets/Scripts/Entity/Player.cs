using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityStatus
{
    public Player(float hp, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine) : base(hp, moveSpeed, attackSpeed, alcohol, caffeine, nicotine) { }
    public float invincibleTime;
    public GameObject angle;
    public List<GameObject> weaponPrefabs;
    public List<GameObject> weapons;
    public GameObject weaponSpawnPos;
    public bool isMelee = true;
    private GameObject weapon;
    private float nextAttack;

    Camera mainCamera;

    private void Start()
    {
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
        if (!Inventory.inventoryActivated) {
            LookAt();
            WeaponSwap();
            Attack();
            if(hp<=0) EntityDie();
        }
        //LookAt();
        //WeaponSwap();
        //Attack();
    }
    void FixedUpdate()
    {
        float xinput = Input.GetAxis("Horizontal");
        float yinput = Input.GetAxis("Vertical");
        Vector2 newVelocity = new Vector2(xinput, yinput);
        gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity*moveSpeed;
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
            StartCoroutine(Swing(angle));
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item.Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                //Debug.Log(hitObject.transform.GetComponent<Consumable>().item.itemName + " ȹ�� �߽��ϴ�.");
                theInventory.AcquireItem(hitObject.transform.GetComponent<Consumable>().item);
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "Weapon(Monster)" && !isInvincible)
        {
            Debug.Log("플레이어 피격");
            Item.DamageHolder currentDamageHolder = collision.GetComponent<Item.Weapon>().GetDamageHolder();
            EntityHit(currentDamageHolder);
            StartCoroutine(InvincibleMode(invincibleTime));
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
