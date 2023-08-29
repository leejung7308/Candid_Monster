using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject radarObject;
    public float moveSpeed = 5f;
    public Vector2 spawnPoint;
    Radar radar;
    GameObject player;
    private void Start()
    {
        radar = radarObject.GetComponent<Radar>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
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
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}