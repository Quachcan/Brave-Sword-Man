using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerController controller;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    //private bool isAttacking = true;
    void Update()
    {
        //if (isAttacking)
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        Attack1();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Mouse1))
        //    {
        //        Attack2();
        //    }
        //}
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack1();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                Attack2();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                Attack3();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack1()
    {
        //Player is attacking
        //isAttacking = true;
        //Play animation Attack1
        animator.SetTrigger("Attack1");
        //Return movement controller
        //StartCoroutine(EndAttack());
    }
    void Attack2() 
    {
        //Player is attacking
        //isAttacking = true;
        //Playe animation Attack2
        animator.SetTrigger("Attack2");
        //Return movement controller
       // StartCoroutine(EndAttack());
    }
    void Attack3()
    {
        //Player is attacking
        //isAttacking = true;
        //Playe animation Attack2
        animator.SetTrigger("Attack3");
        //Return movement controller
        // StartCoroutine(EndAttack());
    }

    //private IEnumerator EndAttack()
    //{
    //    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

    //    isAttacking = false;
    //}
}
