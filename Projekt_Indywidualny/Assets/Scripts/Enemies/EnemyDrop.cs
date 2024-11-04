using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int chanceForCoins = 30;
    [SerializeField] private int minCoinAmount = 1;
    [SerializeField] private int maxCoinAmount = 5;

    [SerializeField] private GameObject CoinPrefab;

    [SerializeField] private bool dropsBits = false;
    [SerializeField] private int minBitAmount = 1;
    [SerializeField] private int maxBitAmount = 5;
    [SerializeField] private GameObject bitPrefab;

    public void Drop()
    {
        int rand = Random.Range(0, 100);
        if (rand < chanceForCoins)
        {
            DropCoins();
        }

        if (dropsBits)
        {
            DropBits();
        }

    }

    public void DropCoins()
    {
        int amount = Random.Range(minCoinAmount, maxCoinAmount);

        for (int i = 0; i < 100; i++)
        {
            Debug.Log("spawning some coin");
            int coinValueIndex = Random.Range(0, CoinPickup.PossibleCoinValues.Length);
            if (CoinPickup.PossibleCoinValues[coinValueIndex] > amount)
            {
                coinValueIndex = 0;
            }

            Vector3 coinOffset = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));

            GameObject coin = Instantiate(CoinPrefab, transform.position + coinOffset, Quaternion.identity);
            CoinPickup coinScript = coin.GetComponentInChildren<CoinPickup>();
            coinScript.SetCoinAmount(CoinPickup.PossibleCoinValues[coinValueIndex]);

            amount -= CoinPickup.PossibleCoinValues[coinValueIndex];

            if (amount <= 0)
            {
                break;
            }


        }

    }

    public void DropBits()
    {
        int amount = Random.Range(minBitAmount, maxBitAmount);

        for (int i = 0; i < amount; i++)
        {
           
            Vector3 bitOffset = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));

            GameObject bit = Instantiate(bitPrefab, transform.position + bitOffset, Quaternion.identity);

            if (amount <= 0)
            {
                break;
            }


        }

    }
}
