using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalMove = 0f;
    public float movementSpeed = 20f;
    //private bool isFacingRight = true;
    public float jumpHeight = 3f;
    bool isGrounded = true;

    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        FlipSprite();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpHeight);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }

    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2 (horizontalMove * movementSpeed , rb.velocity.y);
    }

    void FlipSprite()
    {
        if (horizontalMove > 0f)
        {
            transform.right = Vector3.right;
        }
        else if (horizontalMove < 0f)
        {
            transform.right = Vector3.left;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", !isGrounded);
        }
        //isGrounded = true;
        //animator.SetBool("isJumping", !isGrounded);
    }
}
