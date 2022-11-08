using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char2 : MonoBehaviour
{
    public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 10f;
    public bool isFacingLeft = true;

    public int maxHealth = 5;
    private int currentHealth;

    public float attackRange = 1.3f;
    public float attackRate = 1f;
    float nextAttackTime = 0f;
    public int currentDmg = 1;

    public Transform groundCheck;
    public Transform attackPoint;
    public LayerMask groundLayer;
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask enemyLayers;

    public HealthBar2 healthBar;

    Vector2 movimiento;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void RecibirDano(int dano)
    {
        currentHealth -= dano;
        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int getCurrentHealth()
    {
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

        horizontal = Input.GetAxisRaw("Horizontal1");



        movimiento.x = horizontal;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {

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

    private void Attack()
    {

        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {

            enemy.GetComponent<Char1>().RecibirDano(currentDmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Flip()
    {

        if (isFacingLeft && horizontal > 0f || !isFacingLeft && horizontal < 0f)
        {

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

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public int getAttack()
    {
        return currentDmg;
    }

    public void setAttack(int attack)
    {
        this.currentDmg = attack;

    }
}

