using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopPassiveItem : PassiveItemPickup
{
    [SerializeField] private int cost = 0;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip itemBoughtClip;
    [SerializeField] private AudioClip cardDeclinedClip;
    [SerializeField] private TextMeshPro costText;

    public override void Start()
    {
        base.Start();
        DetermineCost();
    }
    public override void InteractWithPlayer()
    {
        if(PlayerStatus.Instance.Coins < cost)
        {
            audioSource.clip = cardDeclinedClip;
            audioSource.Play();
            return;
        }


        if (item is PassiveItem)
        {
            PlayerStatus.Instance.RemoveCoins(cost);
            AudioSource.PlayClipAtPoint(itemBoughtClip,transform.position); //To jest shit, trzeba to póŸniej zmienic aby mixery dzia³a³y na sourcie
            base.InteractWithPlayer();
        }
        else
        {
            Debug.LogError("You configured something wrong, you used passiveItem pickup with item that is not a passiveItem");
        }

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
        switch (item.Rarity){
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
