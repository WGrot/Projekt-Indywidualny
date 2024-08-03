using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBarImage;
    [SerializeField] TextMeshProUGUI currentHealth;
    [SerializeField] Image playerFace;
    [SerializeField] private Sprite healtyFace;
    [SerializeField] private Sprite beatenFace;

    private void OnEnable()
    {
        //PlayerStatus.OnPlayerHealCallback += ShowHealthData;
        //PlayerStatus.OnPlayerTakeDamageCallback += ShowHealthData;
        PlayerStatus.OnPlayersHealthChangedCallback += ShowHealthData;
        PL_HealthStat.OnHealthModifierAddedCallback+= ShowHealthData;
    }

    private void OnDisable()
    {
        //PlayerStatus.OnPlayerHealCallback -= ShowHealthData;
        //PlayerStatus.OnPlayerTakeDamageCallback -= ShowHealthData;
        PlayerStatus.OnPlayersHealthChangedCallback -= ShowHealthData;
        PL_HealthStat.OnHealthModifierAddedCallback -= ShowHealthData;
    }
    private void Start()
    {
        ShowHealthData();
    }

    private void ShowHealthData()
    {
        float currentHealthData = PlayerStatus.Instance.GetCurrentHp();
        float maxHpData = PlayerStatus.Instance.stats[0].value;
        if(currentHealthData/maxHpData < 0.3)
        {
            playerFace.sprite= beatenFace;
        }
        else
        {
            playerFace.sprite= healtyFace;
        }
        healthBarImage.fillAmount = currentHealthData / maxHpData;
        currentHealth.SetText(currentHealthData.ToString() + "/" + maxHpData);
    }
}
