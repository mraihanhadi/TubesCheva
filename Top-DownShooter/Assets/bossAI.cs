using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
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
    private Vector3 originalFirePointPosition;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        if (!isAttacking)
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
        yield return new WaitForSeconds(0.4f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * 7.5f, ForceMode2D.Impulse);
        animator.SetBool("Shooting",false);
        isAttacking = false;
    }
}
