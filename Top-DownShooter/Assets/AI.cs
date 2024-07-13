using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Animator animator;
    public float damage = 10f;
    private SpriteRenderer spriteRenderer;
    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position); 
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            if (distance < 4)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                animator.SetBool("isWalking",true);
                if(direction.x > 0)
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
                animator.SetBool("isWalking",false);
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isWalking",false);
        animator.SetBool("isAttacking",true);

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        yield return new WaitForSeconds(0.5f);
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        animator.SetBool("isAttacking",false);
        isAttacking = false;
    }
}
