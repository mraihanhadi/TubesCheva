using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrademanager : MonoBehaviour
{
    public playerxp Playerxp;
    public playerStats stats;
    public void IncreaseFireRate()
    {
        stats.fireRate *= 0.95f;
        Debug.Log("Fire cooldown: " + stats.fireRate);
        Playerxp.ResumeGame();
    } 
    public void IncreaseMaxHP()
    {
        stats.maxHP *= 1.15f;
        Debug.Log("Max HP: " + stats.maxHP);
        Playerxp.ResumeGame();
    }
    public void IncreaseDamage()
    {
        stats.damage *= 1.25f;
        Debug.Log("Damage: " + stats.damage);
        Playerxp.ResumeGame();
    }
}
