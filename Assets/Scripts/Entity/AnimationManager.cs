using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AttackStart()
    {
        animator.SetBool("isAttack", true);
    }
    public void HitStart()
    {
        transform.parent.GetComponent<EntityStatus>().hitRange.SetActive(true) ;
    }
    public void HitEnd()
    {
        transform.parent.GetComponent<EntityStatus>() .hitRange.SetActive(false) ;
        transform.parent.GetComponent<Collider2D>().enabled = false ;
    }
    public void AttackEnd()
    {
        transform.parent.GetComponent<EntityStatus>().hitRange.SetActive(false);
        animator.SetBool("isAttack", false);
    }
    public void MoveStart()
    {
        animator.SetBool("isMove", true);
    }
    public void MoveEnd() 
    {
        animator.SetBool("isMove", false);
    }
    public void EntityDieStart()
    {
        animator.SetBool("isDead", true);
    }
    public void EntityDieEnd()
    {
        animator.SetBool("isDead", false);
        transform.parent.gameObject.SetActive(false);
    }
}
