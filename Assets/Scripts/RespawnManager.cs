using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{  
    public GameObject respawnPrefab; // 몬스터 프리팹
    public Transform[] respawnPoints; // 리스폰 지점
    public GameObject respawnTrigger; // 회복실 오브젝트

    public bool isRespawn = false; // 몬스터 리스폰 여부
    
    public GameObject room;

    void Start()
    {
        // 게임 시작 시 몬스터 생성
        SpawnMonster();
    }

     // Update is called once per frame
    void Update()
    {
        if(room.GetComponent<EntityManager>().monsterCount == 0)
        {
            isRespawn = true;
        }
        else isRespawn = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == respawnTrigger)
        {
            // 회복실과 상호 작용하면 몬스터 리스폰 활성화
            if(isRespawn) SpawnMonster();
            isRespawn = false;

        }
    }

    void SpawnMonster()
    {
        // 리스폰 지점 중 하나를 선택
        for(int i = 0; i < respawnPoints.Length; i++) {
            // 몬스터 생성
            Instantiate(respawnPrefab, respawnPoints[i].position, Quaternion.identity);
            room.GetComponent<EntityManager>().monsterCount ++;
        }

        

        Debug.Log("몬스터 스폰");

    }
}
