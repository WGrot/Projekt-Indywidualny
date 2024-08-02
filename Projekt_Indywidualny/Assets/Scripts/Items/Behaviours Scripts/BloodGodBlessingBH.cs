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

    public override void OnPlayerTakeDamage()
    {
        if (!isEffectActive)
        {
            isEffectActive = true;
            CharacterStat stat = PlayerStatus.Instance.stats[3];
            stat.AddModifier(statModifier);

        }
    }

    public override void OnDrop()
    {
        if (isEffectActive)
        {
            CharacterStat stat = PlayerStatus.Instance.stats[3];
            stat.RemoveModifier(statModifier);
            isEffectActive = false;
        }
        PlayerStatus.Instance.ReduceCurrentPlayerHP(20);

    }
    private void OnEnable()
    {
        isEffectActive= false;
    }
    public override void OnPickup()
    {
        PlayerStatus.Instance.IncreaseCurrentPlayerHP(20);
    }
}
