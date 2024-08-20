using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleSlot : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float animTime;
    [SerializeField] private int animSwaps;
    private AudioSource audioSource;
    GambleRewards reward = GambleRewards.None;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Animate(GambleRewards reward)
    {
        this.reward = reward;
        StartCoroutine("AnimateSlot");

    }
    
    IEnumerator AnimateSlot()
    {
        for (int i = 0; i< animSwaps; i++ )
        {
            audioSource.Play();
            int rand = Random.Range(0, sprites.Count);
            spriteRenderer.sprite = sprites[rand];
            yield return new WaitForSeconds(animTime);
        }
        SetCorrectSprite(reward);
    }

    public void SetCorrectSprite(GambleRewards reward)
    {
        switch (reward)
        {
            case GambleRewards.None:
                spriteRenderer.sprite = sprites[0];
                break;
            case GambleRewards.Coins:
                spriteRenderer.sprite = sprites[1];
                break;
            case GambleRewards.Item:
                spriteRenderer.sprite = sprites[2];
                break;
            case GambleRewards.Weapon:
                spriteRenderer.sprite = sprites[3];
                break;

        }
    }

}
