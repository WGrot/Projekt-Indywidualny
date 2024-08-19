using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum GambleRewards
{
    None,
    Coins
}

public class GambleTerminal : MonoBehaviour
{
    private int bid = 1;
    [SerializeField] private TextMeshPro bidCounter;
    [SerializeField] private GambleSlot slot1;
    [SerializeField] private GambleSlot slot2;
    [SerializeField] private GambleSlot slot3;
    [SerializeField] private int chanceForWin = 25;
    GambleRewards reward = GambleRewards.None;
    private AudioSource audioSource;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip InsertCoinSound;

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
            return;
        }

        isGamblin = true;
        Debug.Log("Lets go gambling");
        audioSource.clip = InsertCoinSound;
        audioSource.Play();
        int rand = Random.Range(0, 100);
        Debug.Log(rand);
        if (rand < chanceForWin)
        {
            reward = GambleRewards.Coins;
            Debug.Log("yopu win");
            slot1.Animate(reward);
            slot2.Animate(reward);
            slot3.Animate(reward);
            StartCoroutine("PlayWinSound");

        }
        else
        {
            reward = GambleRewards.None;
            Debug.Log("yo lose");

            slot1.Animate(reward);
            slot2.Animate(reward);
            slot3.Animate(reward);
            StartCoroutine("PlayLoseSound");

        }

    }


    IEnumerator PlayWinSound()
    {
        yield return new WaitForSeconds(1.25f);
        audioSource.clip = winSound;
        audioSource.Play();
        isGamblin= false;
    }

    IEnumerator PlayLoseSound()
    {
        yield return new WaitForSeconds(1.25f);
        audioSource.clip = loseSound;
        audioSource.Play();
        isGamblin= false;
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
