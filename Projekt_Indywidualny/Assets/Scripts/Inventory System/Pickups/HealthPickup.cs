using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, I_Interactable
{
    [SerializeField] Sprite bigHealthSprite;
    [SerializeField] Sprite mediumHealthSprite;
    [SerializeField] Sprite smallHealthSprite;
    SpriteRenderer spriteRenderer;
    BoxCollider objectCollider;
    [SerializeField] int amount;


    [SerializeField] bool isHealthAmountRandom = true;
    [SerializeField] float chanceForBigHealth;
    [SerializeField] float chanceForSmallHealth;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<BoxCollider>();
        if (isHealthAmountRandom)
        {
            int randInt = Random.Range(0, 100);
            if (randInt <= chanceForBigHealth) //bierzemy du¿e ammo
            {
                amount = 100;
            }
            else if(randInt > chanceForBigHealth && randInt <= chanceForSmallHealth + chanceForBigHealth)
            {
                amount = 50;
            }
            else
            {
                amount = 25;
            }
        }

        DetermineSpriteAndSize();
    }


    void DetermineSpriteAndSize()
    {
        if (amount == 25)
        {
            spriteRenderer.sprite = smallHealthSprite;
        }

        if (amount == 50)
        {
            spriteRenderer.sprite = mediumHealthSprite;
            objectCollider.size = new Vector3(1.6f, 0.875f, 0.25f);
        }

        if (amount == 100)
        {
            spriteRenderer.sprite = bigHealthSprite;
            objectCollider.size = new Vector3(2, 2, 0.25f);
        }
    }

    public void InteractWithPlayer()
    {
        float actualAmount = (amount / 100f) * PlayerStatus.Instance.stats[0].value; 
        PlayerStatus.Instance.Heal(actualAmount);
        Destroy(gameObject);
    }
}
