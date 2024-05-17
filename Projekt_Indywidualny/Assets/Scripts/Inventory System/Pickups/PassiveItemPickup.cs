using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemPickup : ItemPickup
{
    [SerializeField] private bool isItemRandom = true;

    public override void InteractWithPlayer()
    {
        Debug.Log("interacted");
        if (item is PassiveItem)
        {
            Inventory.Instance.AddPassiveItem((PassiveItem)item);
        }
        else
        {
            Debug.LogError("You configured something wrong, you used passiveItem pickup with item that is not a passiveItem");
        }
        Destroy(gameObject);
    }



    public override void Start()
    {

        if (isItemRandom)
        {
            ChooseRandomPassiveItem();
        }

        if (base.item != null)
        {
            itemSprite.sprite = item.icon;
        }

    }

    private void ChooseRandomPassiveItem()
    {
        int result = Random.Range(1, 100);
        if (result < 50) //50% chance for common weapon
        {
            Object[] ItemsSOs = Resources.LoadAll("ItemsResources/Items/PassiveItems/Common");
            PassiveItem[] commonItems = new PassiveItem[ItemsSOs.Length];
            ItemsSOs.CopyTo(commonItems, 0);

            base.item = commonItems[Random.Range(0, ItemsSOs.Length)];

        }
        else if (result >= 50 && result < 80) // 30% chance for uncommon weapon
        {
            Object[] ItemsSOs = Resources.LoadAll("ItemsResources/Items/PassiveItems/Uncommon");
            PassiveItem[] uncommonItems = new PassiveItem[ItemsSOs.Length];
            ItemsSOs.CopyTo(uncommonItems, 0);

            base.item = uncommonItems[Random.Range(0, ItemsSOs.Length)];
        }
        else if (result >= 80 && result < 95) // 15% chance for rare weapon
        {
            Object[] ItemsSOs = Resources.LoadAll("ItemsResources/Items/PassiveItems/Rare");
            PassiveItem[] rareItems = new PassiveItem[ItemsSOs.Length];
            ItemsSOs.CopyTo(rareItems, 0);

            base.item = rareItems[Random.Range(0, ItemsSOs.Length)];
        }
        else // Last 5% chance for Mythic weapon
        {
            Object[] ItemsSOs = Resources.LoadAll("ItemsResources/Items/PassiveItems/Mythic");
            PassiveItem[] mythicItems = new PassiveItem[ItemsSOs.Length];
            ItemsSOs.CopyTo(mythicItems, 0);

            base.item = mythicItems[Random.Range(0, ItemsSOs.Length)];
        }
    }
}
