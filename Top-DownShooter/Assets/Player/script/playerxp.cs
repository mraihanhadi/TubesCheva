using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class playerxp : MonoBehaviour
{
    public int currentLevel = 1;
    public float currentExp = 0;
    public float exptonextlevel = 20;
    public UnityEngine.UI.Image expBar;
    public GameObject upgradeMenu;
    // Start is called before the first frame update
    void Start()
    {
        upgradeMenu.SetActive(false);
        currentExp = 0;
        updatexpbar();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentExp >= exptonextlevel)
        {
            LevelUp();
        }
    }

    public void GainXP(int amount)
    {
        currentExp += amount;
        updatexpbar();
    }

    public void updatexpbar()
    {
        if(expBar != null)
        {
            expBar.fillAmount = currentExp/exptonextlevel;
        }
    }

    void LevelUp()
    {
        currentExp -= exptonextlevel;
        currentLevel++;
        exptonextlevel = Mathf.RoundToInt(exptonextlevel * 1.5f);
        PauseGame();
        ShowUpgradeMenu();
        updatexpbar();
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ShowUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}