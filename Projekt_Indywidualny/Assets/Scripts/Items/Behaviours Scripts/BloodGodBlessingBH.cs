using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/BloodGodBlessing")]
public class BloodGodBlessingBH : ItemBehaviour
{
    private static float DamageIncrease = 0.25f;
    private float BuffTime = 2f;
    Buff bloodGodBlessingBuff;
    [SerializeField] Sprite buffIcon;

    public override void OnPlayerTakeDamage()
    {
        bloodGodBlessingBuff = new Buff(BuffTime, DamageIncrease, StatType.Damage, this, StatModType.Flat, Time.time, buffIcon);
        BuffManager.Instance.AddBuff(bloodGodBlessingBuff);
    }

    public override void OnPickup()
    {
        if (!wasPickedUpBefore)
        {
            PlayerStatus.Instance.IncreaseCurrentPlayerHP(20);
        }
        base.OnPickup();
    }
}
