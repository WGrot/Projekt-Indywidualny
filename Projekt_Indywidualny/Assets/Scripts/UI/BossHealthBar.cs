using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

    [SerializeField] Image health;
    [SerializeField] GameObject healthBar;
    [SerializeField] Image bossIcon;

    private void OnEnable()
    {
        BossHpBase.OnBossfightStartedCallback += ShowHealthBar;
        BossHpBase.OnBossfighEndedCallback += HideHealthBar;
        BossHpBase.OnBossHpChangedCallback += UpdateHealthBarValue;
    }

    private void OnDisable()
    {
        BossHpBase.OnBossfightStartedCallback -= ShowHealthBar;
        BossHpBase.OnBossfighEndedCallback -= HideHealthBar;
        BossHpBase.OnBossHpChangedCallback -= UpdateHealthBarValue;
    }

    public void ShowHealthBar(Sprite bossIcon)
    {
        healthBar.SetActive(true);
        this.bossIcon.sprite= bossIcon;

    }
    public void HideHealthBar()
    {
        healthBar.SetActive(false);
    }

    public void UpdateHealthBarValue(float currentHp, float maxHp)
    {
        health.fillAmount = currentHp / maxHp;
    }

}
