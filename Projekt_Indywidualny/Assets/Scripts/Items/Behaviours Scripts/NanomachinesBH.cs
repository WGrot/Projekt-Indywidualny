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
        nanomachinesBuff = new Buff(BuffTime, ArmorBoostValue, StatType.Armor, this, StatModType.Flat, Time.time, buffIcon);
        BuffManager.Instance.AddBuff(nanomachinesBuff);
    }
    
    public override void OnPickup()
    {
        PlayerStatus.Instance.IncreaseCurrentPlayerHP(40);
    }
    

    public override void OnDrop()
    {
        PlayerStatus.Instance.ReduceCurrentPlayerHP(40);
    }
    
}
