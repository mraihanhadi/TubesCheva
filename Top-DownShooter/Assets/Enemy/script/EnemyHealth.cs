using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using JetBrains.Annotations;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    public Image healthBar;
    public Canvas enemyCanvas; 
    public float healthBarVisibleDuration = 2f; 
    public event Action OnEnemyDefeated;
    public Animator animator;
    public AudioSource deathSfx;
    public float xpItemdropChance = 1f;
    public float healthItemdropChance = 1f;
    public GameObject doubleXpItemPrefab;
    public GameObject healthItemPrefan;

    private Coroutine hideHealthBarCoroutine;
    void Start()
    {
        manager enemyStats = FindObjectOfType<manager>();
        maxHealth = enemyStats.enemyHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();
        enemyCanvas.enabled = false; 
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {            
            Die();
        }
        else
        {
            ShowHealthBar();
        }
    }
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }


    void Die()
    {
        OnEnemyDefeated.Invoke();
        TryDropDoubleXpItem();
        tryDropHP();
        gameObject.GetComponent<AIEnemy>().enabled = false;
        gameObject.GetComponent<AIrange>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        deathSfx.Play();
        animator.SetTrigger("dead");
        StartCoroutine(DeathHandler());
    }

    IEnumerator DeathHandler()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void ShowHealthBar()
    {
        if (enemyCanvas != null)
        {
            enemyCanvas.enabled = true;

            if (hideHealthBarCoroutine != null)
            {
                StopCoroutine(hideHealthBarCoroutine);
            }

            hideHealthBarCoroutine = StartCoroutine(HideHealthBarAfterDelay());
        }
    }
    IEnumerator HideHealthBarAfterDelay()
    {
        yield return new WaitForSeconds(healthBarVisibleDuration);
        enemyCanvas.enabled = false;
    }
    public void increaseHp()
    {
        maxHealth *= 1.5f;
    }
    void TryDropDoubleXpItem()
    {
        if (UnityEngine.Random.value <= xpItemdropChance)
        {
            Vector3 dropPosition = GetRandomDropPosition();
            Instantiate(doubleXpItemPrefab, dropPosition, Quaternion.identity);
        }
    }
    void tryDropHP()
    {
        if(UnityEngine.Random.value <= healthItemdropChance)
        {
            Vector3 dropPosition = GetRandomDropPosition();
            Instantiate(healthItemPrefan, dropPosition, Quaternion.identity);
        }
    }
    Vector3 GetRandomDropPosition()
    {
        float dropRadius = 0.15f;
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * dropRadius;
        Vector3 dropPosition = new Vector3(transform.position.x + randomOffset.x, transform.position.y + randomOffset.y, transform.position.z);
        return dropPosition;
    }

}
