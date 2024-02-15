using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Build.Player;
using UnityEngine;
public class Interact_heal : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.G;
    public bool canInteract = false;

    EntityStatus player;

    public List<RespawnManager> respawnmanagers;
    public List<EntityManager> entitymanagers;

    public GameObject interactionFloating;
    private GameObject playerPosition;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStatus>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(interactionKey))
        {
            // 상호작용 스크립트 실행
            ExecuteInteraction();
        }
    }

    //Player Acessable
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionFloating.SetActive(true);
            interactionFloating.transform.position = Camera.main.WorldToScreenPoint(playerPosition.transform.position + new Vector3(1.8f, 1, 0));
            canInteract = true;
        }
    }

    //Player Unacessable
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionFloating.SetActive(false);
            canInteract = false;
        }
    }

   

    private void ExecuteInteraction()
    {
        Debug.Log("힐 상호작용이 실행되었습니다!");
        // 추가적인 상호작용 로직을 이곳에 구현
        
        //플레이어 스탯 상태 초기화
        player.fatigue = 0.0f;
        player.nicotine = 0.0f;
        player.caffeine = 0.0f;
        player.alcohol = 0.0f;


        //몬스터 리스폰
        for(int i = 0; i < respawnmanagers.Count; i++) {
            if(respawnmanagers[i].GetComponent<RespawnManager>().isRespawn) {
                Debug.Log(respawnmanagers[i].name + "의 몬스터가 리젠");
                respawnmanagers[i].GetComponent<RespawnManager>().SpawnMonsters();
                respawnmanagers[i].GetComponent<RespawnManager>().isRespawn = false;
                
            }
        }

        //엿듣기 상태 초기화
        for (int i = 0; i < entitymanagers.Count; i++)
        {
            for (int j = 0; j < entitymanagers[i].sniffs.Count; j++)
            {
                entitymanagers[i].sniffs[j].gameObject.SetActive(true);
            }
            Debug.Log(respawnmanagers[i].name + "의 엿듣기 장소가 활성화");
        }
    }
}