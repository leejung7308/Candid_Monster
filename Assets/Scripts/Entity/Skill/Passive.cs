using Item;
using UnityEngine;

namespace Entity.Skill
{
    /**
     * 스킬 > 알코올 > 폭탄주 제조
     * 무기의 3회 당 추가 타(속성 버프)
     */
    public class BombAlcohol: DamagePassive
    {
        const int additionalHit = 3;            // 추가타를 적용할 임계치
        int hitCount = 0;                       // 현재 누적 타수
        
        /**
         * 누적 타수가 임계치에 도달할 때 마다, 추가타를 적용한다.
         * 추가타는 단순히 데미지를 2배로 만들어 구현한다.
         */
        public override DamageHolder ApplyDamageChange(DamageHolder damageHolder)
        {
            hitCount += 1;
            if(hitCount >= additionalHit)
            {
                Debug.Log($"추가타 발생! 이전 데미지: {damageHolder.Damage}");
                hitCount = 0;
                damageHolder *= new DamageHolder(damage: 2);
                Debug.Log($"적용된 데미지: {damageHolder.Damage}");
            }
            return damageHolder;
        }
    }
    
    /**
     * 스킬 > 커피 > 각성
     * 자신의 커피 스택이 80 이상일 때, 공격 속도와 이동 속도를 증가시킨다.
     */
    public class CoffeBoost : PlayserStatusPassive
    {
        const float attackSpeedRatio = 1.35f;
        const float moveSpeedRatio = 1.35f;
        bool applied = false;
        EntityStatusData originalStatus;
        
        public CoffeBoost(EntityStatus target) : base(target)
        {
            originalStatus = new EntityStatusData(target);
        }
        
        public override void ApplyStatusChange()
        {
            if(target.caffeine >= 80)
            {
                if(!applied)
                {
                    Debug.Log("PassiveSkill > CoffeBoost > Apply Boost!");
                    target.moveSpeed *= moveSpeedRatio;
                    target.attackSpeed *= attackSpeedRatio;
                    applied = true;
                }
            }
            else if (applied)
            {
                Debug.Log("PassiveSkill > CoffeBoost > Remove Boost...");
                originalStatus.Apply(target);
                applied = false;
            }
        }
    }
    
    /**
     * 스킬 > 니코틴 > 담배 타임
     * 기본적으로 피로도가 조금씩 차는 증상을 상쇄한다.
     */
    public class SmokingTime : PlayserStatusPassive
    {
        public SmokingTime(Player target) : base(target) {}
        public override void ApplyStatusChange()
        {
            ((Player)target).enableFatigue = false;
        }
    }
}
