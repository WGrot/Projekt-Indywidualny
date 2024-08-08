using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] AudioClip PickupSound;
    [SerializeField] AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip= PickupSound;
            audioSource.Play();
            PlayerStatus.Instance.AddCoins(amount);
        }
    }

    public void SetCoinAmount(int amount)
    {
        this.amount = amount;
    }
}
