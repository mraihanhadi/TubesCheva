using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public playerStats stats;
    public float maxhealth;
    public UnityEngine.UI.Image healthBar;
    public Animator animator;
    public Shoot fire;
    public playerMovement movement;
    public GameObject deathmenuUI;
    public AudioSource hitSfx;
    private float currenthealth;
    
    // Start is called before the first frame update
    void Start()
    {
        maxhealth = stats.maxHP;
        currenthealth = maxhealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if(currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }
    }

    public void GainHealth(int amount)
    {
        currenthealth += amount;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currenthealth -= amount;
        currenthealth = Mathf.Clamp(currenthealth,0,maxhealth);
        hitSfx.Play();
        UpdateHealthBar();

        if(currenthealth <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    public void UpdateHealthBar()
    {
        if(healthBar != null)
        {
            healthBar.fillAmount = currenthealth/maxhealth;
        }
    }

    void Die()
    {
        movement.enabled = false;
        fire.enabled = false;
        animator.SetTrigger("death");
        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1.5f);
        StopAllEnemies();
        deathmenuUI.SetActive(true);
    }

    void StopAllEnemies()
    {
        AIEnemy[] enemiesmelee = FindObjectsOfType<AIEnemy>();
        foreach (AIEnemy enemy in enemiesmelee)
        {
            enemy.enabled = false;
        }
        AIrange[] enemiesrange = FindObjectsOfType<AIrange>();
        foreach (AIrange enemy in enemiesrange)
        {
            enemy.enabled = false;
        }
        BossAI bosss = FindObjectOfType<BossAI>();
        bosss.enabled = false;
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Quit()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
