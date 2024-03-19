using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemPickup : ItemPickup
{
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
}
