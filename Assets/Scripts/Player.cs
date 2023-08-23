using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float weaponSpeed = 10f;
    public List<GameObject> weapons;
    public GameObject weaponSpawnPos;
    private GameObject weapon;
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        weapon = weapons[0];
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
        Vector2 dirVec = mousePos - (Vector2)transform.position;
        transform.right = dirVec.normalized;
    }
    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("무기1번");
            weapon = weapons[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("무기2번");
            weapon = weapons[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("무기3번");
            weapon = weapons[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("무기4번");
            weapon = weapons[3];
        }

    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 shootingDir = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            shootingDir.Normalize();
            GameObject shootingObject = Instantiate(weapon);
            shootingObject.transform.position = weaponSpawnPos.transform.position;
            shootingObject.GetComponent<Rigidbody2D>().velocity = shootingDir * weaponSpeed;
        }
    }
}
