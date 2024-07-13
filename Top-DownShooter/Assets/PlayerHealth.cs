using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxhealth = 100f;
    public UnityEngine.UI.Image healthBar;
    private float currenthealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currenthealth -= amount;
        currenthealth = Mathf.Clamp(currenthealth,0,maxhealth);
        UpdateHealthBar();

        if(currenthealth <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void UpdateHealthBar()
    {
        if(healthBar != null)
        {
            healthBar.fillAmount = currenthealth/maxhealth;
        }
    }

    void Die()
    {
        
    }
}
