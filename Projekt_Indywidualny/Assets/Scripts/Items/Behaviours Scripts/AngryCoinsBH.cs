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
            Buff angryCoinsBuff = new Buff(buffTime, (amount * buffMultiplier /100), StatType.Damage, this, StatModType.Flat, Time.time, buffIcon);
            BuffManager.Instance.AddBuff(angryCoinsBuff);
        }
    }
}
