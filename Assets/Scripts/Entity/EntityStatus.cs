using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;

/**
 * EntityStatus 객체의 몇몇 상태 정보들을 복사해두고 적용하기 위한 Data Object.
 */
public class EntityStatusData
{
    public float fatigue;
    public float moveSpeed;
    public float attackSpeed;
    public float maxFatigue;
    public float maxAlcohol;
    public float maxCaffeine;
    public float maxNicotine;

    public EntityStatusData(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue, int maxAlcohol, int maxCaffeine, int maxNicotine)
    {
        this.fatigue = fatigue;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.maxFatigue = maxFatigue;
        this.maxAlcohol = maxAlcohol;
        this.maxCaffeine = maxCaffeine;
        this.maxNicotine = maxNicotine;
    }
    
    public EntityStatusData(EntityStatus entity)
    {
        Copy(entity);
    }
    
    public void Copy(EntityStatus entity)
    {
        fatigue = entity.fatigue;
        moveSpeed = entity.moveSpeed;
        attackSpeed = entity.attackSpeed;
        maxFatigue = entity.maxFatigue;
        maxAlcohol = entity.maxAlcohol;
        maxCaffeine = entity.maxCaffeine;
        maxNicotine = entity.maxNicotine;
    }

    public void Apply(EntityStatus entity)
    {
        entity.fatigue = fatigue;
        entity.moveSpeed = moveSpeed;
        entity.attackSpeed = attackSpeed;
        entity.maxFatigue = maxFatigue;
        entity.maxAlcohol = maxAlcohol;
        entity.maxCaffeine = maxCaffeine;
        entity.maxNicotine = maxNicotine;
    }
}

public class EntityStatus : MonoBehaviour
{
    public float fatigue;
    public float moveSpeed;
    public float attackSpeed;
    public float alcohol;
    public float caffeine;
    public float nicotine;
    public float maxFatigue;
    public float maxAlcohol;
    public float maxCaffeine;
    public float maxNicotine;
    public bool isConfused = false;
    public bool isFainted = false;
    public bool isInvincible = false;
    public bool isDebuffed = false;
    public GameObject weapon;
    
    public EntityStatus(float fatigue, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine, float maxFatigue, float maxAlcohol, float maxCaffeine, float maxNicotine)
    {
        this.fatigue = fatigue;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.alcohol = alcohol;
        this.caffeine = caffeine;
        this.nicotine = nicotine;
        this.maxFatigue = maxFatigue;
        this.maxAlcohol = maxAlcohol;
        this.maxCaffeine = maxCaffeine;
        this.maxNicotine = maxNicotine;
    }
    protected void EntityHit(DamageHolder currentDamageHolder)
    {
        alcohol += currentDamageHolder.Alcohol;
        caffeine += currentDamageHolder.Caffeine;
        nicotine += currentDamageHolder.Nicotine;
        fatigue += currentDamageHolder.Damage;
        if (CheckDebuffCondition() != DebuffType.None) isDebuffed = true;
        Debug.Log($"{gameObject.name} 은 {currentDamageHolder.Damage} 만큼의 피해를 입었다!");
    }
    protected IEnumerator InvincibleMode(float time)
    {
        Color originalColor = weapon.GetComponent<SpriteRenderer>().color;
        isInvincible = true;
        if (!isDebuffed)
        {
            for (int i = 0; i < 6; i++)
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
            weapon.GetComponent<SpriteRenderer>().color = originalColor - new Color(0, 0, 0, 0.3f);
        }
        yield return new WaitForSeconds(time);
        if (!isDebuffed)
        {
            for (int i = 0; i < 6; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); ;
            }
            weapon.GetComponent<SpriteRenderer>().color = originalColor;
        }
        isInvincible = false;
    }
    virtual protected void EntityDie()
    {
            gameObject.SetActive(false);
    }
    virtual protected void ApplyDebuff(DebuffType type)
    {
        switch (type)
        {
            case DebuffType.Alcohol:
                Debug.Log("Debuff > Activate Alcohol Debuff!");
                gameObject.GetComponent<Debuff>().StartAlcoholDebuff();
                alcohol = 0;
                break;
            case DebuffType.Caffeine:
                Debug.Log("Debuff > Activate Caffeine Debuff!");
                gameObject.GetComponent<Debuff>().StartCaffeineDebuff();
                caffeine = 0;
                break;
            case DebuffType.Nicotine:
                Debug.Log("Debuff > Activate Nicotine Debuff!");
                gameObject.GetComponent<Debuff>().StartNicotineDebuff();
                nicotine = 0;
                break;
            case DebuffType.Mark:
                Debug.Log("Debuff > Activate Mark Debuff!");
                gameObject.GetComponent<Debuff>().StartMarkDebuff();
                break;
        }
    }
    public DebuffType CheckDebuffCondition()
    {
        if (alcohol >= maxAlcohol) return DebuffType.Alcohol;
        else if (caffeine >= maxCaffeine) return DebuffType.Caffeine;
        else if (nicotine >= maxNicotine) return DebuffType.Nicotine;
        else return DebuffType.None;
    }
    
    /**
     * 엔티티가 입힐 수 있는 현재 피해량을 담은 DamageHolder를 계산한다.
     */
    public virtual DamageHolder GetDamageHolder() => new DamageHolder();

}
