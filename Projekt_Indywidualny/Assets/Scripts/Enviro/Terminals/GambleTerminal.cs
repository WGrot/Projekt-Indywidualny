using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GambleRewards
{
    None,
    Coins,
    Item,
    Weapon
}

public class GambleTerminal : MonoBehaviour
{

    [SerializeField] private TextMeshPro bidCounter;
    [SerializeField] private GambleSlot slot1;
    [SerializeField] private GambleSlot slot2;
    [SerializeField] private GambleSlot slot3;


    [SerializeField] private int chanceForCoins = 25;
    [SerializeField] private int chanceForGoodReward = 10;
    [SerializeField] private int chanceForItem = 75;
    [SerializeField] private int chanceForWeapon = 25;
    [SerializeField] private GameObject weaponPickupPrefab;
    [SerializeField] private GameObject itemPickupPrefab;
    [SerializeField] private GameObject itemSpawnPoint;

    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip gambleDeniedSound;
    [SerializeField] AudioClip InsertCoinSound;

    GambleRewards reward = GambleRewards.None;
    private AudioSource audioSource;
    private int bid = 1;
    private bool isGamblin = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGamblin()
    {
        if (isGamblin)
        {
            return;
        }

        if (PlayerStatus.Instance.Coins >= bid)
        {
            PlayerStatus.Instance.RemoveCoins(bid);
        }
        else
        {
            audioSource.clip = gambleDeniedSound;
            audioSource.Play();
            return;
        }

        isGamblin = true;
        audioSource.clip = InsertCoinSound;
        audioSource.Play();




        int rand = Random.Range(0, 100);
        Debug.Log(rand);


        chanceForGoodReward = bid;
         
        if (rand < chanceForCoins)
        {
            reward = GambleRewards.Coins;
            AnimateSlots(reward);
            StartCoroutine("WinCoins");

        }
        else if (rand > 100 - chanceForGoodReward)
        {

            int chance = Random.Range(0, 100);

            if (chance < chanceForWeapon)
            {
                reward = GambleRewards.Weapon;
            }
            else
            {
                reward= GambleRewards.Item;
            }

            AnimateSlots(reward);
            StartCoroutine("WinSpecial");
        }
        else
        {
            reward = GambleRewards.None;
            AnimateSlots(reward);
            StartCoroutine("Lose");

        }
        
    }

    private void AnimateSlots(GambleRewards reward)
    {
        slot1.Animate(reward);
        slot2.Animate(reward);
        slot3.Animate(reward);
    }
    IEnumerator WinCoins()
    {
        yield return new WaitForSeconds(1.25f);
        audioSource.clip = winSound;
        audioSource.Play();
        isGamblin= false;
        PlayerStatus.Instance.AddCoins(2 * bid);
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(1.25f);
        audioSource.clip = loseSound;
        audioSource.Play();
        isGamblin= false;
    }

    IEnumerator WinSpecial()
    {
        yield return new WaitForSeconds(1.25f);
        audioSource.clip = winSound;
        audioSource.Play();
        if(reward == GambleRewards.Item)
        {
            Instantiate(itemPickupPrefab, itemSpawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(weaponPickupPrefab, itemSpawnPoint.transform.position, Quaternion.identity);
        }
        isGamblin = false;
        
    }
   
    

    public void IncreaseBid()
    {
        bid++;
        if(bid > 99)
        {
            bid = 1;
        }
        UpdateBidCounter();
    }

    public void DecreaseBid()
    {
        bid--;
        if (bid < 1)
        {
            bid = 99;
        }
        UpdateBidCounter();
    }

    public void ResetBid()
    {
        bid = 1;
    }

    public int GetBid()
    {
        return bid;
    }


    public void UpdateBidCounter()
    {
        bidCounter.text = bid.ToString();
    }
}
