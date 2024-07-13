using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    public Image healthBar;  // Reference to the HealthBar UI Image
    public Canvas enemyCanvas;  // Reference to the EnemyCanvas
    public float healthBarVisibleDuration = 2f;  // Duration for which the health bar remains visible

    private Coroutine hideHealthBarCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        enemyCanvas.enabled = false;  // Initially hide the health bar
    }

    // Method to reduce health
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

    // Method to update the health bar UI
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    // Method to handle enemy death
    void Die()
    {
        // Handle enemy death (e.g., destroy, respawn)
        Destroy(gameObject);
    }

    // Method to show the health bar and start the coroutine to hide it
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

    // Coroutine to hide the health bar after a delay
    IEnumerator HideHealthBarAfterDelay()
    {
        yield return new WaitForSeconds(healthBarVisibleDuration);
        enemyCanvas.enabled = false;
    }
}
