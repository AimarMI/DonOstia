using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char1 : MonoBehaviour
{
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 25f;
    public bool isFacingRight = true;


    public int maxHealth = 5;
    private int currentHealth;

    public float attackRange = 0.7f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Transform groundCheck;
    public Transform attackPoint;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask enemyLayers;

    public HealthBar1 healthBar;

    Vector2 movimiento;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void RecibirDaño(int daño)
    {
        currentHealth -= daño;
        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int getCurrentHealth() {
        return currentHealth;
    }

    void Die()
    {


        animator.SetBool("IsDead", true);
        this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        horizontal =  Input.GetAxisRaw("Horizontal");


        movimiento.x = horizontal;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
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

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            
            enemy.GetComponent<Char2>().RecibirDaño(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
