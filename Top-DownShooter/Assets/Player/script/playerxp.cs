using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class playerxp : MonoBehaviour
{
    public int currentLevel = 1;
    public float currentExp = 0;
    public float exptonextlevel = 20;
    public UnityEngine.UI.Image expBar;
    public GameObject upgradeMenu;
    public TextMeshProUGUI leveltext;
    public bool isDoubleXpActive = false;
    public float doubleXpDuration = 30f;
    public TextMeshProUGUI doublexpDurationText;
    private float remainingBuffTime;
    private Coroutine doubleXpCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        upgradeMenu.SetActive(false);
        doublexpDurationText.gameObject.SetActive(false);
        currentExp = 0;
        updatexpbar();
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoubleXpActive)
        {
            remainingBuffTime -= Time.deltaTime;
            doublexpDurationText.text = "2x XP: " + remainingBuffTime.ToString("F2") + "s";
            if (remainingBuffTime <= 0)
            {
                DeactivateDoubleXp();
            }
        }
        if (currentExp >= exptonextlevel)
        {
            LevelUp();
        }
    }

    public void GainXP(float amount)
    {
        if (isDoubleXpActive)
        {
            amount *= 2;
        }
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
        exptonextlevel = Mathf.RoundToInt(exptonextlevel * 1.75f);
        updateText();
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
    void updateText()
    {
        leveltext.text = $"Level {currentLevel}";
    }

    public void ResumeGame()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ActivateDoubleXp()
    {
        if(isDoubleXpActive)
        {
            StopCoroutine(doubleXpCoroutine);
        }
        doubleXpCoroutine = StartCoroutine(DoubleXpCoroutine());
    }

    IEnumerator DoubleXpCoroutine()
    {
        isDoubleXpActive = true;
        remainingBuffTime = doubleXpDuration;
        doublexpDurationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(doubleXpDuration);
        DeactivateDoubleXp();
    }
    private void DeactivateDoubleXp()
    {
        isDoubleXpActive = false;
        doublexpDurationText.gameObject.SetActive(false);
    }
}