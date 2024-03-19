using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : ItemPickup
{
    public override void InteractWithPlayer()
    {
        if (item is WeaponSO)
        {
            Inventory.Instance.AddWeapon((WeaponSO)item);
        }
        else
        {
            Debug.LogError("You configures something wrong, you used weapon pickap with item that is not a weapon");
        }
        Destroy(gameObject);
    }
}
