using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/AngryCoinsBH")]
public class AngryCoinsBH : ItemBehaviour
{
    [SerializeField] Sprite buffIcon;
    [SerializeField] private float buffTime;
    [SerializeField] private float buffMultiplier;
    public override void OnCoinAmountChange(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        if (amount > 0)
        {
            StatModifier statModifier = new StatModifier(amount * buffMultiplier / 100, StatType.Damage, StatModType.Flat, 100, this);
            Buff angryCoinsBuff = new Buff(buffTime, statModifier, this, Time.time, buffIcon);
            BuffManager.Instance.AddBuff(angryCoinsBuff);
        }
    }
}
