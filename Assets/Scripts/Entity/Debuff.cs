using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum DebuffType
{
    None,
    Alcohol,
    Caffeine,
    Nicotine,
    Mark
}

public class Debuff : MonoBehaviour
{
    [SerializeField]
    float AlcholoDebuffDuration = 15f;
    [SerializeField]
    float CaffeineDebuffDuration = 15f;
    [SerializeField]
    int NicotineDebuffSecs = 5;
    [FormerlySerializedAs("MarkDebuffSes"),SerializeField]
    int MarkDebuffSec = 5;
    
    // AoE에서 지속적으로 Debuff가 적용되므로, Debuff Coroutine들이 무한히 생기는것을 방지하기 위해, 기존 Coroutine을 기록하고 삭제한다.
    // TODO: Coroutine 실행시간을 기억했다가 종료 전인걸 파악하면 진행중인 Coroutine의 남은 시간을 갱신하는 건?
    Coroutine prevAlcoholCoro;
    Coroutine prevCaffeineCoro;
    Coroutine prevNicotineCoro;
    Coroutine prevMarkCoro;
    
    public IEnumerator AlcoholDebuff()
    {
        gameObject.GetComponent<EntityStatus>().isConfused = true;
        yield return new WaitForSeconds(15f);
        gameObject.GetComponent <EntityStatus>().isConfused = false;
        prevAlcoholCoro = null;
    }
    public IEnumerator CaffeineDebuff()
    {
        gameObject.GetComponent<EntityStatus>().isFainted = true;
        yield return new WaitForSeconds(15f);
        gameObject.GetComponent <EntityStatus>().isFainted = false;
        prevCaffeineCoro = null;
    }
    public IEnumerator NicotineDebuff()
    {
        for (int i = 0; i < 5; i++) {
            gameObject.GetComponent<EntityStatus>().fatigue += 2;
            yield return new WaitForSeconds(1f);
        }
        prevNicotineCoro = null;
    }
    public IEnumerator MarkDebuff()
    {
        for (int i = 0; i < MarkDebuffSec; i++) {
            gameObject.GetComponent<EntityStatus>().nicotine += 2;
            yield return new WaitForSeconds(1f);
        }
        prevMarkCoro = null;
    }
    public void StartAlcoholDebuff()
    {
        if (prevAlcoholCoro != null)
            StopCoroutine(prevAlcoholCoro);
        prevAlcoholCoro = StartCoroutine(AlcoholDebuff());
    }
    public void StartCaffeineDebuff()
    {
        if (prevCaffeineCoro != null)
            StopCoroutine(prevCaffeineCoro);
        prevCaffeineCoro = StartCoroutine (CaffeineDebuff());
    }
    public void StartNicotineDebuff()
    {
        if (prevNicotineCoro != null)
            StopCoroutine(prevNicotineCoro);
        prevNicotineCoro = StartCoroutine (NicotineDebuff());
    }
    public void StartMarkDebuff()
    {
        if (prevMarkCoro != null)
            StopCoroutine(prevMarkCoro);
        prevMarkCoro = StartCoroutine (MarkDebuff());
    }
}
