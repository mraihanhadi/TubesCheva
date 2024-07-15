using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIrange : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Animator animator;
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float shootingRange = 6f; 
    public float fireRate = 1f; 

    private SpriteRenderer spriteRenderer;
    private float nextFireTime;
    private bool isAttacking;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        nextFireTime = Time.time;
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if(!isAttacking)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if(distance > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isWalking",true);
            }

            if (distance < shootingRange && Time.time >= nextFireTime)
            {
                StartCoroutine(ShootProjectile(direction));
                nextFireTime = Time.time + 1f / fireRate;
            }
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        } 
    }

    IEnumerator ShootProjectile(Vector2 direction)
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("shoot",true);
        isAttacking = true;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 1.5f, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("shoot",false);
        isAttacking = false;
    }
}
