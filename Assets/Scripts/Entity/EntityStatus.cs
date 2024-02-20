using System.Collections;
using System.Collections.Generic;
using Item;
using Unity.VisualScripting;
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

    public EntityStatusData(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue)
    {
        this.fatigue = fatigue;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.maxFatigue = maxFatigue;
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
    }

    public void Apply(EntityStatus entity)
    {
        entity.fatigue = fatigue;
        entity.moveSpeed = moveSpeed;
        entity.attackSpeed = attackSpeed;
        entity.maxFatigue = maxFatigue;
    }
}

public class EntityStatus : MonoBehaviour
{
    public float fatigue;
    public float moveSpeed;
    public float attackSpeed;
    public float maxFatigue;
    public bool isConfused = false;
    public bool isFainted = false;
    public bool isInvincible = false;
    public bool isDebuffed = false;
    public GameObject hitRange;
    public GameObject weapon;
    public List<GameObject> parts = new List<GameObject>();
    
    public EntityStatus(float fatigue, float moveSpeed, float attackSpeed, float maxFatigue)
    {
        this.fatigue = fatigue;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.maxFatigue = maxFatigue;
    }

    protected void EntityHit(DamageHolder currentDamageHolder, float rate)
    {
        fatigue += (currentDamageHolder.Damage * rate);
        Debug.Log($"{gameObject.name} 은 {currentDamageHolder.Damage * rate} 만큼의 피해를 입었다!");
    }

    protected IEnumerator InvincibleMode(float time)
    {
        Color originalColor = weapon.GetComponent<SpriteRenderer>().color;
        List<Color> originalColors = new List<Color>();
        isInvincible = true;
        if (!isDebuffed)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                originalColors.Add(parts[i].GetComponent<SpriteRenderer>().color);
                parts[i].GetComponent<SpriteRenderer>().color = originalColors[i] - new Color(0, 0, 0, 0.3f);
            }
            weapon.GetComponent<SpriteRenderer>().color = originalColor - new Color(0, 0, 0, 0.3f);
        }
        yield return new WaitForSeconds(time);
        if (!isDebuffed)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].GetComponent<SpriteRenderer>().color = originalColors[i];
            }
            weapon.GetComponent<SpriteRenderer>().color = originalColor;
        }
        isInvincible = false;
    }
    virtual protected void EntityDie()
    {
        transform.GetChild(0).GetComponent<AnimationManager>().EntityDieStart();
    }
    
    /**
     * 엔티티가 입힐 수 있는 현재 피해량을 담은 DamageHolder를 계산한다.
     */
    public virtual DamageHolder GetDamageHolder() => new DamageHolder();

}
