using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSystem : MonoBehaviour
{
    public float currentXp;
    [SerializeReference] private float maxXp = 100;

    [SerializeField] private Slider xpBar;
    [SerializeField] private TMP_Text xpText;
    private int currentLevel = 1 ;

    [SerializeField] private GameObject upgradeUIScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXPUI();
        if (currentXp >= maxXp)
        {
            LevelUp();
        }
    }

    private void UpdateXPUI()
    {
        xpText.text = currentXp + "/" + maxXp;
        xpBar.value = currentXp;
        xpBar.maxValue = maxXp;
    }

    private void LevelUp()
    {
        upgradeUIScreen.SetActive(true);
        currentLevel += 1;
        currentXp = 0;
        maxXp = currentLevel + 10 * 2;
        Time.timeScale = .01f;
    }

    public void UpgradeMaxHealth()
    {
        GameObject.FindObjectOfType<CarController>().maxHp += 10;
        GameObject.FindObjectOfType<CarController>().hp = GameObject.FindObjectOfType<CarController>().maxHp;
        ResumeGamepay();
    }

    public void UpgradeDashSpeed()
    {
        GameObject.FindObjectOfType<CarAbiliete>().dashSpeed += 10;
        ResumeGamepay();
    }

    public void UpgradeDashTime()
    {
        GameObject.FindObjectOfType<CarAbiliete>().dashTime += 0.5f;
        ResumeGamepay();
    }
    
    private void ResumeGamepay()
    {
        Time.timeScale = 1f;
        upgradeUIScreen.SetActive(false);
    }
}
