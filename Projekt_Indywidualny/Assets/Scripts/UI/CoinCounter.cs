using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        PlayerStatus.OnCoinsAmountChangeCallback += ChangeCoinCounter;
        LevelGenerationV2.OnLevelGenerated += ChangeCoinCounter;
    }

    private void OnDisable()
    {
        PlayerStatus.OnCoinsAmountChangeCallback -= ChangeCoinCounter;
        LevelGenerationV2.OnLevelGenerated -= ChangeCoinCounter;
    }


    public void ChangeCoinCounter(int amount)
    {
        text.text = PlayerStatus.Instance.Coins.ToString();
    }

    public void ChangeCoinCounter()
    {
        text.text = PlayerStatus.Instance.Coins.ToString();
    }
}
