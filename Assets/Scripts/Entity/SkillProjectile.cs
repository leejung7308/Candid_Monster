using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    [SerializeField]
    DebuffType damageType = DebuffType.None;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Monster") || other.CompareTag("Player") || other.CompareTag("ConfusedMonster"))
        {
            HandleProjectileHit(other.GetComponent<EntityStatus>());
            Destroy(this);
        }
    }
    
    /**
     * 투사체 피격을 처리한다.
     */
    void HandleProjectileHit(EntityStatus target)
    {
        switch(damageType)
        {
            case DebuffType.Caffeine:
                target.caffeine += 10;
                break;
            case DebuffType.Mark:
                target.GetComponent<Debuff>().StartMarkDebuff();
                break;
        }
    }
}
