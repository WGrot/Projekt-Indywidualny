using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private int chanceForCoins = 30;
    [SerializeField] private int minCoinAmount = 1;
    [SerializeField] private int maxCoinAmount = 5;

    [SerializeField] private GameObject CoinPrefab;
    

    public void Drop()
    {
        DropCoins();
    }
    
    public void DropCoins()
    {
        GameObject coin = Instantiate(CoinPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
        CoinPickup coinScript = coin.GetComponentInChildren<CoinPickup>();

        int amount = Random.Range(minCoinAmount, maxCoinAmount);

        coinScript.SetCoinAmount(amount);
    }
}
