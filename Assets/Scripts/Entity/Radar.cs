using UnityEngine;

public class Radar : MonoBehaviour
{
    public Monster monster;
    bool isBlinded = false;
    float blindTimer = 0.0f;
    
    void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
    }
    void Update()
    {
        if (isBlinded)
            CheckBlindTimer();
    }
    
    /**
     * 어그로 해제 (Blind)의 Timer를 관리한다.
     */
    void CheckBlindTimer()
    {
        blindTimer += Time.deltaTime;
        if(blindTimer >= 2.0f)
        {
            isBlinded = false;
            blindTimer = 0.0f;
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("BlindArea"))
            Blind();
        
        if(isBlinded)   // 어그로 풀린 상태일 경우, 움직이지 않는다.
            return;

        if(collision.CompareTag("ConfusedMonster"))
        {
            monster.MonsterMovement(collision.gameObject);
            monster.LookAt(collision.gameObject);
        }
        else if(monster.isConfused && collision.CompareTag("Monster"))
        {
            monster.MonsterMovement(collision.gameObject);
            monster.LookAt(collision.gameObject);
        }
        else if(collision.CompareTag("Player"))
        {
            monster.MonsterMovement(collision.gameObject);
            monster.LookAt(collision.gameObject);
        }
    }

    public void Blind()
    {
        if(isBlinded)
            blindTimer = 0.0f;
        else
            isBlinded = true;
    }
}
