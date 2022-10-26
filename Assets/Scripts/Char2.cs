using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char2 : MonoBehaviour
{
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 10f;
    public bool isFacingLeft = true;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movimiento;




    // Update is called once per frame
    void Update()
    {

        horizontal =  Input.GetAxisRaw("Horizontal1");
        


        movimiento.x = horizontal;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) {

            animator.SetTrigger("Teleport");
            

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Teleport"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
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

        if (isFacingLeft && horizontal > 0f || !isFacingLeft && horizontal < 0f) {

            if (isFacingLeft)
            {
                transform.position = new Vector2(transform.position.x + 3.5f, transform.position.y);
            }
            else 
            {
                transform.position = new Vector2(transform.position.x - 3.5f, transform.position.y);
            }


            isFacingLeft = !isFacingLeft;
            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}

