using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char1 : MonoBehaviour
{
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 25f;
    public bool isFacingRight = true;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movimiento;




    // Update is called once per frame
    void Update()
    {

        horizontal =  Input.GetAxisRaw("Horizontal");


        movimiento.x = horizontal;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.S)) {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("isJumping", true);

        }

        if (rb.velocity.y < 0f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }

        if (IsGrounded())
        {
            animator.SetBool("isFalling", false);
        }

        
        Flip();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Attack() {

        animator.SetTrigger("Attack");
    }

    private void Flip() {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {

            if (isFacingRight)
            {
                transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
            }
            else 
            {
                transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
            }
            

            isFacingRight = !isFacingRight;
            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
