using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interact_heal : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.G;
    public bool canInteract = false;

    EntityStatus player;

    //Player Acessable
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    //Player Unacessable
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
    private void Start()
    {
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

    private void ExecuteInteraction()
    {
        Debug.Log("상호작용이 실행되었습니다!");
        // 추가적인 상호작용 로직을 이곳에 구현
        
        player.fatigue = 0.0f;
        player.nicotine = 0.0f;
        player.caffeine = 0.0f;
        player.alcohol = 0.0f;
        
    }
}