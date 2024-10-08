using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/IHS_BH")]
public class IndomitableHumanSpiritBehaviour : ItemBehaviour
{
    public float DamageMultiplier = 2f;
    public float SpeedMultiplier = 2f;
    public float ReloadSpeedMultiplier = 2f;
    public float StaminaMultiplier = 2f;
    public float hpTheshold = 0.1f;
    private float BuffTime = 2f;
    Buff bloodGodBlessingBuff;
    [SerializeField] Sprite buffIcon;

    public override void OnPlayerTakeDamage()
    {
        List<StatModifier> modifiers = new List<StatModifier>
        {
            new StatModifier(DamageMultiplier, StatType.Damage, StatModType.PercentMulti, 300, this),
            new StatModifier(SpeedMultiplier, StatType.MoveSpeed, StatModType.PercentMulti, 300, this),
            new StatModifier(ReloadSpeedMultiplier, StatType.ReloadSpeed, StatModType.PercentMulti, 300, this),
            new StatModifier(StaminaMultiplier, StatType.Stamina, StatModType.PercentMulti, 300, this)
        };
        bloodGodBlessingBuff = new IndefiniteBuff(modifiers, this, Time.time, buffIcon, CondFunc);
        BuffManager.Instance.AddBuff(bloodGodBlessingBuff);
    }

    public bool CondFunc()
    {
        if (PlayerStatus.Instance.GetCurrentHp() < PlayerStatus.Instance.stats[0].value * hpTheshold)
        {
            return true;
        }

        return false;
    }
}
