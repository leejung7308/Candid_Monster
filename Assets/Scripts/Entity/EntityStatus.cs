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
    protected IEnumerator InvincibleMode(float time)
    {
        Color originalColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        isInvincible = true;
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.4f, 1);
            }
            yield return new WaitForSeconds(time / 10);
            for (int i = 0; i < 6; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 0.4f, 1);
            }
            yield return new WaitForSeconds(time / 10);
        }
        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = originalColor;
        }
        isInvincible = false;
    }
    virtual protected void EntityDie()
    {
            gameObject.SetActive(false);
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
