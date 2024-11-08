using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockBackSpeedX, knockBackSpeedY, knockBackDuration, knockBackDeadSpeedX, knockBackDeadSpeedY, deathTorque;
    [SerializeField]
    private bool applyKnockBack;

    private float currenHealth, knockBackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockBack;

    private PlayerController3 controller;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator animator;

    private void Start()
    {
        currenHealth = maxHealth;

        controller = GameObject.Find("NamelessSwordMan").GetComponent<PlayerController3>();

        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        animator = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockBack();
    }

    private void Damage(float amount)
    {
        currenHealth -= amount;
        playerFacingDirection = controller.GetFacingDirection();
        


        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        animator.SetBool("PlayerOnLeft", playerOnLeft);
        animator.SetTrigger("Damage");
        
        if (applyKnockBack && currenHealth > 0.0f)
        {
            KnockBack();
        }

        if (currenHealth < 0.0f)
        {
            Die();
        }
    }

    private void KnockBack()
    {
        knockBack = true;
        knockBackStart = Time.time;
        rbAlive.velocity = new Vector2(knockBackSpeedX * playerFacingDirection, knockBackSpeedY);
    }

    private void CheckKnockBack()
    {
        if (Time.time >= knockBackStart + knockBackDuration && knockBack)
        {
            knockBack = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockBackSpeedX * playerFacingDirection, knockBackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockBackDeadSpeedX * playerFacingDirection, knockBackDeadSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
