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
    void Start()
    {
        UpdateUI();
    }
    public void IncreaseFireRate()
    {
        stats.fireRate *= 0.95f;
        UpdateUI();
        Playerxp.ResumeGame();
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
        maxHPText.text = $"Max HP: {stats.maxHP:F2} -> {stats.maxHP * 1.15f:F2}";
        damageText.text = $"Damage: {stats.damage:F2} -> {stats.damage * 1.15f:F2}";
    }
}
