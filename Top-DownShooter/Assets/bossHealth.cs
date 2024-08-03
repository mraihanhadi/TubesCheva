using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class bossHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Animator animator;
    public Image healthBar;
    public TextMeshProUGUI currentHealthText;
    public float shieldDamageReduction = 0.25f;
    public event Action OnEnemyDefeated;
    public AudioSource deathsfx;
    private bool isShieldActive;
    private bool isActivatingShield;
    private float healthThreshold;
    // Start is called before the first frame update
    void Start()
    {
        manager bossStats = FindObjectOfType<manager>();
        maxHealth = bossStats.bossHealth;
        currentHealth = maxHealth;
        healthThreshold = maxHealth * 0.5f;
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < 0)
        {
            currentHealth = 0;
            UpdateHealthBar();
        }
    }
    public void TakeDamage(float damage)
    {
        if (isActivatingShield)
        {
            return;
        }
        if (isShieldActive)
        {
            damage *= shieldDamageReduction;
        }
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= healthThreshold && !isActivatingShield)
        {
            ActivateShield();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        OnEnemyDefeated.Invoke();
        gameObject.GetComponent<BossAI>().enabled = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        animator.SetTrigger("Death");
        deathsfx.Play();
        StartCoroutine(DeathHandler());
    }

    IEnumerator DeathHandler()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
    }
    void UpdateHealthBar()
    {
        bossHealth currentHP = FindObjectOfType<bossHealth>();
        currentHealthText.text = $"{currentHP.currentHealth} / {currentHP.maxHealth}";
        healthBar.fillAmount = currentHP.currentHealth / currentHP.maxHealth;
    }
    void ActivateShield()
    {
        if (isActivatingShield || isShieldActive)
        {
            return;
        }
        isActivatingShield = true;
        gameObject.GetComponent<BossAI>().enabled = false;
        animator.SetTrigger("activateShield");
        StartCoroutine(ShieldCoroutine());
    }
    IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(4.9f);
        isShieldActive = true;
        isActivatingShield = false;
        gameObject.GetComponent<BossAI>().enabled = true;
        animator.SetBool("shieldActive",true);
        Debug.Log(isActivatingShield);
    }
}
