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
    public void AttackEnd()
    {
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
}
