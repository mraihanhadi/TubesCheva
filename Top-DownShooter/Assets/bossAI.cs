using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Animator animator;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootingRange = 6f;
    public float fireRate = 1f;
    public float shieldDuration = 20f;
    public float shieldDamageReduction = 0.25f;
    public float maxHealth = 100f;
    public float currentHealth;

    private SpriteRenderer spriteRenderer;
    private float nextFireTime;
    private bool isAttacking;
    private bool isShieldActive;
    private Coroutine shieldCoroutine;
    private float healthThreshold;
    private Vector3 originalFirePointPosition;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthThreshold = maxHealth * 0.8f; // 80% of max health

        nextFireTime = Time.time;
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
        originalFirePointPosition = new Vector3(-0.182f,-0.039f,-0.001368514f);
    }

    void Update()
    {
        if (!isAttacking && !isShieldActive)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isWalking", true);
            }

            if (distance < shootingRange && Time.time >= nextFireTime)
            {
                StartCoroutine(ShootProjectile(direction));
                nextFireTime = Time.time + 1f / fireRate;
            }

            // Flip sprite based on direction
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
                firePoint.localPosition = originalFirePointPosition;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
                firePoint.localPosition = new Vector3(-originalFirePointPosition.x, originalFirePointPosition.y, originalFirePointPosition.z);
            }
        }
    }

    IEnumerator ShootProjectile(Vector2 direction)
    {
        animator.SetBool("Shooting",true);
        isAttacking = true;
        yield return new WaitForSeconds(0.425f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 1.5f, ForceMode2D.Impulse);
        animator.SetBool("Shooting",false);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        if (isShieldActive)
        {
            damage *= shieldDamageReduction;
        }

        currentHealth -= damage;

        // Check if health has dropped by 20%
        if (currentHealth <= healthThreshold)
        {
            ActivateShield();
            healthThreshold = currentHealth - maxHealth * 0.2f; // Update threshold to next 20%
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ActivateShield()
    {
        if (!isShieldActive)
        {
            isShieldActive = true;
            shieldCoroutine = StartCoroutine(ShieldCoroutine());
        }
    }

    IEnumerator ShieldCoroutine()
    {
        animator.SetBool("ShieldActive", true);
        yield return new WaitForSeconds(shieldDuration);
        animator.SetBool("ShieldActive", false);
        isShieldActive = false;
    }

    void Die()
    {
        // Handle boss death (e.g., play animation, drop items, etc.)
        Destroy(gameObject);
    }
}
