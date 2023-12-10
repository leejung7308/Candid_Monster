using UnityEngine;

public class AreaOfEffect: MonoBehaviour
{
    public DebuffType debuffType;
    [SerializeField]
    float duration = 5.0f;
    float time = 0.0f;

    void Update()
    {
        time += Time.deltaTime;
        if(time >= duration)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Monster"))
            HandleEffect(other.GetComponent<EntityStatus>());
    }
    /**
     * 이 AoE 영역의 효과를 주어진 개체에 적용한다.
     */
    void HandleEffect(EntityStatus target)
    {
        switch(debuffType)
        {
            case DebuffType.Alcohol:
                target.GetComponent<Debuff>().StartAlcoholDebuff();
                break;
            case DebuffType.Caffeine:
                target.GetComponent<Debuff>().StartCaffeineDebuff();
                break;
            case DebuffType.Nicotine:
                target.GetComponent<Debuff>().StartNicotineDebuff();
                break;
            case DebuffType.Mark:
                target.GetComponent<Debuff>().StartMarkDebuff();
                break;
        }
    }
}
