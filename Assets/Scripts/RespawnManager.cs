using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefab; // 스폰할 몬스터 프리팹
    public GameObject room; //방
    
    public List<Transform> spawnPositions = new List<Transform>(); // 스폰 위치 리스트
    public GameObject respawnTrigger; // 힐 상호작용 오브젝트
    public bool isRespawn; //리스폰 여부
    public int monsterSet; //리스폰매니저에 할당된 몬스터 수
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnMonsters();
        isRespawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(room.GetComponent<EntityManager>().monsterCount <= 0) {
            isRespawn = true;
        }
    }

    public void SpawnMonsters()
    {
        for(int i = 0; i < monsterSet; i++) {
            var monster = monsterPrefab[i];

            Debug.Log(monsterPrefab.Length);

            monster.gameObject.SetActive(true);
            monster.GetComponent<Monster>().fatigue = 0;
            monster.GetComponent<Transform>().position = spawnPositions[i].position;
            monster.GetComponent<Transform>().rotation = spawnPositions[i].rotation;
 
            monster.GetComponent<CapsuleCollider2D>().enabled = true;
            monster.GetComponent<Raycast>().isDetected = false;

            room.GetComponent<EntityManager>().monsterCount++;
           
        }
        
    }
}
