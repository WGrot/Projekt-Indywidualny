using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/SpiteAmunitionBH")]
public class SpiteAmunitionBH : ItemBehaviour
{
    [SerializeField] private int percentRefill = 5;
    public override void OnSuccessfulParry()
    {
        Inventory.Instance.GetAmmoOfActiveWeapon().RefillByPercent(percentRefill);
        base.OnSuccessfulParry();
    }
}
