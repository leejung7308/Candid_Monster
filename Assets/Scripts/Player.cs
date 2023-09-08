using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject angle;
    public float moveSpeed;
    public float weaponSpeed = 10f;
    public float attackCoolDown = 0.5f;
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
        LookAt();
        WeaponSwap();
        Attack();
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
        if (weaponNum != 0)
        {
            weapon.GetComponent<Collider2D>().enabled = false;
            isMelee = false;
        }
    }
    void Shoot()
    {
        if (!isMelee)
        {
            Vector2 shootingDir = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            shootingDir.Normalize();
            GameObject shootingObject = Instantiate(weapon);
            shootingObject.GetComponent<Collider2D>().enabled = true;
            shootingObject.transform.position = weaponSpawnPos.transform.position;
            shootingObject.transform.up = shootingDir;
            shootingObject.GetComponent<Rigidbody2D>().velocity = shootingDir * weaponSpeed;
        }
    }
    void Attack()
    {
        if (Input.GetMouseButton(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCoolDown;
            StartCoroutine(Swing());
        }
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
        if(col.tag == "Ranged Weapon(Monster)")
        {
            Debug.Log("플레이어 피격(원거리)");
            Destroy(col.gameObject);

        }
        else if(col.tag == "Melee Weapon(Monster)")
        {
            Debug.Log("플레이어 피격(근거리)");
        }
    }
}
