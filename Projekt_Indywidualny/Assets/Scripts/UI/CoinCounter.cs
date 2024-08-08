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
    }

    private void OnDisable()
    {
        PlayerStatus.OnCoinsAmountChangeCallback -= ChangeCoinCounter;
    }


    public void ChangeCoinCounter()
    {
        text.text = PlayerStatus.Instance.Coins.ToString();
    }
}
