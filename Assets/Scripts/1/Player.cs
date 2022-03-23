using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed=1;
    [SerializeField]
    float jumpPower = 2f;
    Rigidbody2D rb;
    bool isOnGround=true;
    Animator animator;
    SpriteRenderer sr;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        if (rb.velocity.x>0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        Jump();
    }
    void Jump()
    {
        if (Input.GetButton("Jump") && isOnGround )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isInAir", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
            animator.SetBool("isInAir", true);
        }
    }

}
