using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarImage;
    [SerializeField] TextMeshProUGUI currentHealth;

    private void OnEnable()
    {
        PlayerStatus.OnPlayerHealCallback += ShowHealthData;
        PlayerStatus.OnPlayerTakeDamageCallback += ShowHealthData;
    }

    private void OnDisable()
    {
        PlayerStatus.OnPlayerHealCallback -= ShowHealthData;
        PlayerStatus.OnPlayerTakeDamageCallback -= ShowHealthData;
    }
    private void ShowHealthData()
    {
        float currentHealthData = PlayerStatus.Instance.GetCurrentHp();
        float maxHpData = PlayerStatus.Instance.stats[0].value;
        healthBarImage.fillAmount = currentHealthData / maxHpData;
        currentHealth.SetText(currentHealthData.ToString());
    }
}
