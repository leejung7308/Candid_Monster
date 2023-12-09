using UnityEngine;

namespace Entity.Skill
{
    /**
     * 엔티티가 사용할 수 있는 스킬에 대한 개념적인 표현 객체.
     */
    public abstract class Skill
    {
    }
    
    /**
     * 액티브 스킬에 대한 개념적인 표현 객체.
     */
    public class ActiveSkill : Skill
    {
        float cooldown;
        protected bool isCooldown = false;
        float cooldown_left = 0.0f;
        public ActiveSkill(float cooldown = 2.0f)
        {
            this.cooldown = cooldown;
        }
        public virtual void Activate()
        {
            if (!isCooldown)
                StartCooldown();
        }
        
        /**
         * 이 스킬의 쿨다운을 시작한다.
         */
        public void StartCooldown()
        {
            cooldown_left = cooldown;
            isCooldown = true;
        }
        /**
        * 액티브 스킬의 쿨타임을 처리한다.
        */
        public void HandleCooldown()
        {
            if(isCooldown)
            {
                cooldown_left -= Time.deltaTime;
                if(cooldown_left <= 0.0f)
                {
                    isCooldown = false;
                }
            }
        }
        
    }
    
    /**
     * 플레이어의 상태에만 영향을 주는 패시브 스킬.
     */
    public abstract class PlayserStatusPassive : Skill
    {
        protected EntityStatus target;      // 상태 정보를 변경할 엔티티 객체.

        public PlayserStatusPassive(EntityStatus target)
        {
            this.target = target;
        }
        
        /**
         * target의 상태를 변경한다.
         */
        public abstract void ApplyStatusChange();
    }

    /**
     * 피격된 몬스터에게 가해질 피해 (무기 데미지, 스택) 에만 영향을 주는 패시브 스킬.
     */
    public abstract class DamagePassive : Skill
    {
        public abstract Item.DamageHolder ApplyDamageChange(Item.DamageHolder damageHolder);
    }
}