using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InteractSniff : MonoBehaviour
{
    public GameObject interactionFloating;
    public List<GameObject> monsters = new List<GameObject>();
    public KeyCode interactionKey = KeyCode.G;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] List<GameObject> chatBubbles = new List<GameObject>();
    bool isCameraMoving = false;
    bool canInteract = false;
    GameObject player;
    GameObject camera;
    GameObject canvas;
    Vector3 targetPosition = Vector3.zero;
    List<string> texts = new List<string>();
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        canvas = GameObject.FindGameObjectWithTag("UI");
        for(int i = 0; i < monsters.Count; i++)
        {
            targetPosition += monsters[i].transform.position;
        }
        targetPosition = targetPosition/monsters.Count + new Vector3(0,0,-10);
        if(monsters.Count == 1)
        {
            chatBubbles.Add(canvas.transform.Find("Chat Bubble_1").gameObject);
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Alcohol) {
                texts.Add("아 진짜 술 먹고\n성불하고 싶다...");
            }
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Caffeine) {
                texts.Add("커피 한 잔 하면\n날아갈 거 같아...");
            }
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Nicotine) {
                texts.Add("니코틴...!\n니코틴이 부족해...!");
            }
        }
        else if(monsters.Count == 2)
        {
            chatBubbles.Add(canvas.transform.Find("Chat Bubble_1").gameObject);
            chatBubbles.Add(canvas.transform.Find("Chat Bubble_2").gameObject);
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Alcohol)
            {
                texts.Add("끝나고 소주 한 잔\n하실까요?");
                texts.Add("키야! 소주 먹고\n광명 찾자!");
            }
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Caffeine)
            {
                texts.Add("커피...\n커피를 주세요...");
                texts.Add("으어...\n나도 좀 주세요...");
            }
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Nicotine)
            {
                texts.Add("담배 한 대면\n성불 각?");
                texts.Add("ㄹㅇㅋㅋ");
            }
        }
        else if(monsters.Count == 3)
        {
            chatBubbles.Add(canvas.transform.Find("Chat Bubble_1").gameObject);
            chatBubbles.Add(canvas.transform.Find("Chat Bubble_2").gameObject);
            chatBubbles.Add(canvas.transform.Find("Chat Bubble_3").gameObject);
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Alcohol)
            {
                texts.Add("소주 마려운데\n저만 그런가요?");
                texts.Add("저도 그렇습니다.");
                texts.Add("나만 그런 게\n아니구나!");
            }
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Caffeine)
            {
                texts.Add("아 개피곤해\n카페인이 딸린다");
                texts.Add("전 아아 부탁해요~");
                texts.Add("그럼 난 아이스 라떼!");
            }
            if (monsters[0].GetComponent<Monster>().monsterType == MonsterType.Nicotine)
            {
                texts.Add("담배 끊은지 3분\n금단현상 온다...");
                texts.Add("나는 30초...\n금단현상 온다...");
                texts.Add("그러지 말고\n피고 오시죠!");
            }
        }
    }
    private void Update()
    {
        if (monsters.Count == 3)
        {
            if (monsters[0].GetComponent<Raycast>().isDetected || monsters[1].GetComponent<Raycast>().isDetected || monsters[2].GetComponent<Raycast>().isDetected)
            {
                monsters[0].GetComponent<Raycast>().isDetected = true;
                monsters[1].GetComponent<Raycast>().isDetected = true;
                monsters[2].GetComponent<Raycast>().isDetected = true;
                gameObject.SetActive(false);
            }
        }
        if (monsters.Count == 2)
        {
            if(monsters[0].GetComponent<Raycast>().isDetected || monsters[1].GetComponent<Raycast>().isDetected)
            {
                monsters[0].GetComponent<Raycast>().isDetected = true;
                monsters[1].GetComponent<Raycast>().isDetected = true;
                gameObject.SetActive(false);
            }
        }
        if(monsters.Count == 1) { }
        {
            if (monsters[0].GetComponent<Raycast>().isDetected)
            {
                gameObject.SetActive(false) ;
            }
        }
        if(isCameraMoving)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, targetPosition, 0.25f);
        }
        if (canInteract&&Input.GetKeyDown(interactionKey))
        {
            StartCoroutine(Sniff());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionFloating.SetActive(true);
            interactionFloating.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + new Vector3(1.8f, 1, 0));
            canInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionFloating.SetActive(false);
            canInteract = false;
        }
    }

    private IEnumerator Sniff()
    {
        canInteract = false;
        interactionFloating.SetActive(false);
        player.GetComponent<EntityStatus>().isInvincible = true;
        player.GetComponent<EntityStatus>().isFainted = true;
        camera.GetComponent<MainCamera>().isSniff = true;
        isCameraMoving = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < monsters.Count; i++)
        {
            chatBubbles[i].gameObject.SetActive(true);
            chatBubbles[i].transform.position = Camera.main.WorldToScreenPoint(monsters[i].transform.position + new Vector3(0, 2f, 0));
            foreach (char s in texts[i])
            {
                chatBubbles[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += s;
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < monsters.Count; i++)
        {
            chatBubbles[i].gameObject.SetActive(false);
            chatBubbles[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        isCameraMoving = false;
        player.GetComponent<EntityStatus>().isInvincible = false;
        player.GetComponent<EntityStatus>().isFainted = false;
        camera.GetComponent<MainCamera>().isSniff = false;
    }
}
