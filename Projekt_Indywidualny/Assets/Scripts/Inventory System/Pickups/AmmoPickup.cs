using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour, I_Interactable
{
    [SerializeField] Sprite mediumAmmoSprite;
    [SerializeField] Sprite bigAmmoSprite;
    SpriteRenderer spriteRenderer;
    BoxCollider collider;
    [SerializeField] int amount;


    [SerializeField] bool isAmmoAmountRandom = true;
    [SerializeField] float chanceForBigAmmo;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider>();
        if (isAmmoAmountRandom)
        {
            int randInt = Random.Range(0, 100);
            if (randInt <= chanceForBigAmmo) //bierzemy du�e ammo
            {
                amount= 100;
            }else
            {
                amount = 50;
            }
        }

        DetermineSpriteAndSize();

    }

    void DetermineSpriteAndSize()
    {
        if (amount == 50)
        {
            spriteRenderer.sprite = mediumAmmoSprite;
        }
        
        if(amount == 100)
        {
            spriteRenderer.sprite = bigAmmoSprite;
            collider.size = new Vector3(2, 2, 0.25f);
        }
    }

    public void InteractWithPlayer()
    {
        Debug.Log("PlayerTry to picup ammo");
    }
}
