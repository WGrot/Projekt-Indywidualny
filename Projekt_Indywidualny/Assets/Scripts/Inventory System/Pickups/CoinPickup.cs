using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] AudioClip PickupSound;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Sprite OneCoinSprite;
    [SerializeField] Sprite ThreeCoinSprite;
    [SerializeField] Sprite FiveCoinSprite;
    [SerializeField] Sprite TenCoinSprite;

    public static int[] PossibleCoinValues = {1,3,5,10};

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = PickupSound;
            audioSource.Play();
            PlayerStatus.Instance.AddCoins(amount);
            gameObject.GetComponent<SphereCollider>().enabled= false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyCoinAfterPickup());
        }
    }

    private IEnumerator DestroyCoinAfterPickup()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject.transform.parent.gameObject); 
    }

    private void DetermineSprite()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (amount)
        {
            case 1:
                spriteRenderer.sprite = OneCoinSprite;
                break;
            case 3:
                spriteRenderer.sprite = ThreeCoinSprite;
                break;
            case 5:
                spriteRenderer.sprite = FiveCoinSprite;
                break;
            case 10:
                spriteRenderer.sprite = TenCoinSprite;
                break;

        }
    }

    public void SetCoinAmount(int amount)
    {
        this.amount = amount;
        DetermineSprite();
    }
}
