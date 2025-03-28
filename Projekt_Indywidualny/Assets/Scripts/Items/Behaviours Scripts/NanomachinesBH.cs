using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/NanomachinesBH")]
public class NanomachinesBH : ItemBehaviour
{
    [SerializeField] private float ArmorBoostValue = 10f;
    [SerializeField] private float BuffTime = 3f;
    Buff nanomachinesBuff;
    [SerializeField] Sprite buffIcon;

    public override void OnPlayerTakeDamage()
    {
        StatModifier statModifier = new StatModifier(ArmorBoostValue,StatType.Armor, StatModType.Flat, 100, this);
        nanomachinesBuff = new Buff(BuffTime, statModifier, this, Time.time, buffIcon);
        BuffManager.Instance.AddBuff(nanomachinesBuff);
    }
    
    public override void OnPickup()
    {
        if (!wasPickedUpBefore)
        {
            PlayerStatus.Instance.IncreaseCurrentPlayerHP(40);
        }
        base.OnPickup();

    }
    
    
}
