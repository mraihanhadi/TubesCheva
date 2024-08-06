using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class upgrademanager : MonoBehaviour
{
    public playerxp Playerxp;
    public playerStats stats;
     public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI maxHPText;
    public TextMeshProUGUI damageText;
    public float minFireRate = 0.1f;
    public bool maxfireRate = false;
    void Start()
    {
        UpdateUI();
    }
    void Update()
    {
        if (stats.fireRate <= minFireRate && !maxfireRate)
        {
            UpdateUI();
            maxfireRate = true;
        }
    }
    public void IncreaseFireRate()
    {
        if (stats.fireRate >= minFireRate)
        {
            stats.fireRate *= 0.95f;
            UpdateUI();
            Playerxp.ResumeGame();
        }
    } 
    public void IncreaseMaxHP()
    {
        stats.maxHP *= 1.15f;
        UpdateUI();
        Playerxp.ResumeGame();
    }
    public void IncreaseDamage()
    {
        stats.damage *= 1.15f;
        UpdateUI();
        Playerxp.ResumeGame();
    }
    void UpdateUI()
    {
        fireRateText.text = $"Fire Rate: {stats.fireRate:F2} -> {stats.fireRate * 0.95f:F2}";
        if (stats.fireRate <= minFireRate)
        {
            fireRateText.text = "Max";
        }
        maxHPText.text = $"Max HP: {stats.maxHP:F2} -> {stats.maxHP * 1.15f:F2}";
        damageText.text = $"Damage: {stats.damage:F2} -> {stats.damage * 1.15f:F2}";
    }
}
