using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus : MonoBehaviour
{
    public float hp;
    public float moveSpeed;
    public float attackSpeed;
    public float alcohol;
    public float caffeine;
    public float nicotine;
    public bool isInvincible = false;
    public EntityStatus(float hp, float moveSpeed, float attackSpeed, float alcohol, float caffeine, float nicotine)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.alcohol = alcohol;
        this.caffeine = caffeine;
        this.nicotine = nicotine;
    }
    protected void EntityHit(Item.DamageHolder currentDamageHolder)
    {
        alcohol += currentDamageHolder.Alcohol;
        caffeine += currentDamageHolder.Caffeine;
        nicotine += currentDamageHolder.Nicotine;
        hp -= currentDamageHolder.Damage;
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
    protected void EntityDie()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
