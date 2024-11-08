using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator weaponAnimator;

    protected virtual void Start()
    {
        weaponAnimator = transform.Find("Sword").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        weaponAnimator.SetBool("attack", true);
    }

    public virtual void  ExitWeapon()
    {
        weaponAnimator.SetBool("attack", false);
        gameObject.SetActive(false);
    }
}
