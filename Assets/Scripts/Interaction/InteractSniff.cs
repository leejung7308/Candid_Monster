using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InteractSniff : MonoBehaviour
{
    public GameObject interactionFloating;
    public List<GameObject> monsters = new List<GameObject>();
    public List<GameObject> chatBubbles = new List<GameObject>();
    public KeyCode interactionKey = KeyCode.G;
    private Vector3 velocity = Vector3.zero;
    bool isCameraMoving = false;
    bool canInteract = false;
    bool chatbubble = false;
    GameObject player;
    GameObject camera;
    Vector3 targetPosition = Vector3.zero;
    List<string> texts = new List<string>();
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        for(int i = 0; i < monsters.Count; i++)
        {
            targetPosition += monsters[i].transform.position;
        }
        targetPosition = targetPosition/monsters.Count + new Vector3(0,0,-10);
        if(monsters.Count == 1)
        {
            if (monsters[0].GetComponent<MonsterTest>().isAlcohol) {
                texts.Add("아 진짜 술 먹고\n성불하고 싶다...");
            }
            if (monsters[0].GetComponent <MonsterTest>().isCaffeine) {
                texts.Add("커피 한 잔 하면\n날아갈 거 같아...");
            }
            if (monsters[0].GetComponent<MonsterTest>().isNicotine) {
                texts.Add("니코틴...!\n니코틴이 부족해...!");
            }
        }
        else if(monsters.Count == 2)
        {
            if (monsters[0].GetComponent<MonsterTest>().isAlcohol)
            {
                texts.Add("끝나고 소주 한 잔\n하실까요?");
                texts.Add("키야! 소주 먹고\n광명 찾자!");
            }
            if (monsters[0].GetComponent<MonsterTest>().isCaffeine)
            {
                texts.Add("커피...\n커피를 주세요...");
                texts.Add("으어...\n나도 좀 주세요...");
            }
            if (monsters[0].GetComponent<MonsterTest>().isNicotine)
            {
                texts.Add("담배 한 대면\n성불 각?");
                texts.Add("ㄹㅇㅋㅋ");
            }
        }
    }
    private void Update()
    {
        if(isCameraMoving)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, targetPosition, 0.05f);
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
        camera.GetComponent<MainCamera>().isSniff = true;
        isCameraMoving = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < monsters.Count; i++)
        {
            chatBubbles[i].gameObject.SetActive(true);
            chatBubbles[i].transform.position = Camera.main.WorldToScreenPoint(monsters[i].transform.position + new Vector3(0, 1.5f, 0)); 
            foreach (char s in texts[i])
            {
                chatBubbles[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text += s;
                yield return new WaitForSeconds(0.05f);
            }
        }
        yield return new WaitForSeconds(1);
        for (int i = 0; i < monsters.Count; i++)
        {
            chatBubbles[i].gameObject.SetActive(false);
            chatBubbles[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        }
        isCameraMoving = false;
        yield return new WaitForSeconds(1);
        player.GetComponent<EntityStatus>().isInvincible = false;
        camera.GetComponent<MainCamera>().isSniff = false;
    }
}
