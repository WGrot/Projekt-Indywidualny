using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/BloodGodBlessing")]
public class BloodGodBlessingBH : ItemBehaviour
{
    private static float DamageIncrease = 0.25f;
    private float BuffTime = 2f;
    private bool isEffectActive = false;
    private StatModifier statModifier = new StatModifier(DamageIncrease, StatModType.Flat, 100);
    Buff bloodGodBlessingBuff;

    public override void OnPlayerTakeDamage()
    {
        bloodGodBlessingBuff = new Buff(BuffTime, DamageIncrease, StatType.Damage, this, StatModType.Flat, Time.time);
        BuffManager.Instance.AddBuff(bloodGodBlessingBuff);
    }

    public override void OnDrop()
    {

        PlayerStatus.Instance.ReduceCurrentPlayerHP(20);

    }
    private void OnEnable()
    {
        isEffectActive = false;
    }
    public override void OnPickup()
    {
        PlayerStatus.Instance.IncreaseCurrentPlayerHP(20);
    }
}
