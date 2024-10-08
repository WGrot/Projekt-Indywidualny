using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/BloodGodBlessing")]
public class BloodGodBlessingBH : ItemBehaviour
{
    public float DamageIncrease = 0.25f;
    public float BuffTime = 2f;
    Buff bloodGodBlessingBuff;
    [SerializeField] Sprite buffIcon;

    public override void OnPlayerTakeDamage()
    {
        StatModifier statModifier = new StatModifier(DamageIncrease,StatType.Damage, StatModType.Flat, 100, this);
        bloodGodBlessingBuff = new Buff(BuffTime, statModifier, this, Time.time, buffIcon);
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
