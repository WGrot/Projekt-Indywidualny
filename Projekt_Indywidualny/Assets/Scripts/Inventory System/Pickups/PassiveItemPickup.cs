using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemPickup : ItemPickup
{
    [SerializeField] private bool isItemRandom = true;

    public override void InteractWithPlayer()
    {
        if (item is PassiveItem)
        {
            Inventory.Instance.AddPassiveItem((PassiveItem)item);
            base.InteractWithPlayer();
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
            PassiveItem item = ItemPoolsManager.Instance.GetRandomPassiveItemFromPool(base.pool);
            if(item != null)
            {
                base.item = item;
            }
        }

        if (item != null)
        {
            itemSprite.sprite = item.icon;
        }

    }
}
