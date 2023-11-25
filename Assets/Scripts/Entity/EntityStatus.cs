using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected void EntityHit(Item.DamageHolder currentDamageHolder)
    {
        alcohol += currentDamageHolder.Alcohol;
        caffeine += currentDamageHolder.Caffeine;
        nicotine += currentDamageHolder.Nicotine;
        fatigue += currentDamageHolder.Damage;
    }
    protected IEnumerator Swing(GameObject angle)
    {
        for (int i = 90; i >= -90; i--)
        {
            angle.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, i);
            yield return new WaitForSeconds(0.001f);
        }
        angle.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

    }
    protected IEnumerator InvincibleMode(float time)
    {
        Color originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        isInvincible = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        isInvincible = false;
    }
    virtual protected void EntityDie()
    {
            Destroy(gameObject);
    }
    virtual protected void ApplyDebuff(int index)
    {
        switch (index)
        {
            case 0:
                gameObject.GetComponent<Debuff>().StartAlcoholDebuff();
                alcohol = 0;
                break;
            case 1:
                gameObject.GetComponent<Debuff>().StartCaffeineDebuff();
                caffeine = 0;
                break;
            case 2:
                gameObject.GetComponent<Debuff>().StartNicotineDebuff();
                nicotine = 0;
                break;
            default: 
                break;

        }
    }
    public int CheckDebuffCondition()
    {
        if (alcohol >= maxAlcohol) return 0;
        else if (caffeine >= maxCaffeine) return 1;
        else if (nicotine >= maxNicotine) return 2;
        else return -1;    
    }
}
