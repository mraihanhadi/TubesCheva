using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    public Image healthBar;
    public Canvas enemyCanvas; 
    public float healthBarVisibleDuration = 2f; 
    public event Action OnEnemyDefeated;

    private Coroutine hideHealthBarCoroutine;

    void Start()
    {
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
}
