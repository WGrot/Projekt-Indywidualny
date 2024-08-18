using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopWeaponPickup : WeaponPickup
{
    [SerializeField] private int cost = 0;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip itemBoughtClip;
    [SerializeField] private AudioClip cardDeclinedClip;
    [SerializeField] private TextMeshPro costText;


    public override void InteractWithPlayer()
    {

        if (PlayerStatus.Instance.Coins < cost)
        {
            audioSource.clip = cardDeclinedClip;
            audioSource.Play();
            return;
        }
        PlayerStatus.Instance.RemoveCoins(cost);
        AudioSource.PlayClipAtPoint(itemBoughtClip, transform.position);
        base.InteractWithPlayer();

    }

    public override void Start()
    {
        base.Start();
        DetermineCost();
    }


    public override void DoWhenLookedAt()
    {
        if (!isIconActive)
        {
            StartCoroutine("ShowPickupIcon");
        }
    }

    IEnumerator ShowPickupIcon()
    {
        pickupIcon.SetActive(true);
        isIconActive = true;
        costText.text = cost.ToString();
        yield return new WaitForSeconds(pickupIconLifeTime);
        pickupIcon.SetActive(false);
        isIconActive = false;
    }

    public void DetermineCost()
    {
        switch (item.Rarity)
        {
            case Rarities.Common:
                cost = ConstantValues.COMMON_ITEM_COST;
                break;
            case Rarities.Uncommon:
                cost = ConstantValues.UNCOMMON_ITEM_COST;
                break;
            case Rarities.Rare:
                cost = ConstantValues.RARE_ITEM_COST;
                break;
            case Rarities.Mythic:
                cost = ConstantValues.MYTHIC_ITEM_COST;
                break;
            case Rarities.Special:
                cost = 69;
                break;


        }
    }
}
