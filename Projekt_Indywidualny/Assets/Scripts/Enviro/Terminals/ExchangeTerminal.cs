using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExchangeTerminal : MonoBehaviour
{
    [SerializeField] private int exchangeRate;
    [SerializeField] private TextMeshPro exchangeRateText;
    private AudioSource audioSource;

    [SerializeField] private AudioClip exchangeSuccessSound;
    [SerializeField] private AudioClip exchangeFailedSound;


    public void Start()
    {
        exchangeRateText.text = exchangeRate.ToString();
        audioSource = GetComponent<AudioSource>();    
    }

    public void Exchange()
    {
        if(PlayerStatus.Instance.Coins < exchangeRate)
        {
            audioSource.clip = exchangeFailedSound;
            audioSource.Play();
            return;
        }

        PlayerStatus.Instance.RemoveCoins(exchangeRate);
        SaveManager.Instance.ChangeAmountOfCollectedBits(1);
        audioSource.clip = exchangeSuccessSound;
        audioSource.Play();
    }
}
