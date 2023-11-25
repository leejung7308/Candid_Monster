using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public IEnumerator AlcoholDebuff()
    {
        gameObject.GetComponent<EntityStatus>().isConfused = true;
        yield return new WaitForSeconds(15f);
        gameObject.GetComponent <EntityStatus>().isConfused = false;
    }
    public IEnumerator CaffeineDebuff()
    {
        gameObject.GetComponent<EntityStatus>().isFainted = true;
        yield return new WaitForSeconds(15f);
        gameObject.GetComponent <EntityStatus>().isFainted = false;
    }
    public IEnumerator NicotineDebuff()
    {
        for (int i = 0; i < 5; i++) {
            gameObject.GetComponent<EntityStatus>().fatigue += 2;
            yield return new WaitForSeconds(1f);
        }
    }
    public void StartAlcoholDebuff()
    {
        StartCoroutine(AlcoholDebuff());
    }
    public void StartCaffeineDebuff()
    {
        StartCoroutine (CaffeineDebuff());
    }
    public void StartNicotineDebuff()
    {
        StartCoroutine (NicotineDebuff());
    }
}
