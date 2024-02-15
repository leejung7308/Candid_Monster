using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RespawnManager : MonoBehaviour
{
    public GameObject[] monsterPrefab; // 스폰할 몬스터 프리팹
    public GameObject room; //방
    
    public List<RectTransform> spawnPositions = new List<RectTransform>(); // 스폰 위치 리스트
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

            monster.gameObject.SetActive(true);
            monster.GetComponent<Monster>().fatigue = 0;
            monster.GetComponent<RectTransform>().position = spawnPositions[i].GetComponent<RectTransform>().position;
            monster.GetComponent<RectTransform>().rotation = spawnPositions[i].GetComponent<RectTransform>().rotation;
            monster.GetComponent<RectTransform>().localScale = spawnPositions[i].localScale;



            monster.GetComponent<CapsuleCollider2D>().enabled = true;
            monster.GetComponent<Raycast>().isDetected = false;

            room.GetComponent<EntityManager>().monsterCount++;
           
        }
        
    }
}
