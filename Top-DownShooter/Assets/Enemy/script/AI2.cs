using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI2 : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Animator animator;
    public float damage = 10f;
    public GameObject laserPrefab; // Add your laser prefab here
    public Transform firePoint; // The point from which the laser will be fired
    public float shootingRange = 6f; // Range within which the enemy will shoot the laser
    public float attackRange = 1f; // Range within which the enemy will attack the player
    public float fireRate = 1f; // Time between shots

    private SpriteRenderer spriteRenderer;
    private bool isAttacking;
    private float nextFireTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        nextFireTime = Time.time;
    }

    void Update()
    {
        if (!isAttacking)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance < shootingRange && Time.time >= nextFireTime)
            {
                StartCoroutine(ShootLaser(direction));
                nextFireTime = Time.time + 1f / fireRate;
            }

            if (distance < attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isWalking", true);
                if (direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    IEnumerator ShootLaser(Vector2 direction)
    {
        // Ensure the enemy is not walking
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        isAttacking = true;

        // Instantiate and shoot the laser
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 2f, ForceMode2D.Impulse); // Adjust force value as needed

        yield return new WaitForSeconds(0.5f); // Adjust delay as needed
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }
}
