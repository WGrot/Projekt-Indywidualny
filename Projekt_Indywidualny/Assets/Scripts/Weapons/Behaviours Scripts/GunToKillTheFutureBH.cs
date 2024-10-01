using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/GunToKillTHeFUtureBH")]

public class GunToKillTheFutureBH : ChargedWeaponBehaviour
{
    public override void OnShoot()
    {
        PlayerStatus.Instance.Die();
        SaveManager.Instance.UnlockItem(0);
    }
}
