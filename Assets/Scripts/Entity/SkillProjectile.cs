using Entity;
using UnityEngine;

public class SkillProjectile : Projectile
{
    [SerializeField]
    DebuffType damageType = DebuffType.None;

    public override void Break(Collider2D other)
    {
        if(other is not null)
            HandleProjectileHit(other.GetComponent<EntityStatus>());
        Destroy(gameObject);
    }
    
    /**
     * 투사체 피격을 처리한다.
     */
    void HandleProjectileHit(EntityStatus target)
    {
        switch(damageType)
        {
            case DebuffType.Caffeine:
                break;
            case DebuffType.Mark:
                target.GetComponent<Debuff>().StartMarkDebuff();
                break;
        }
    }
}
