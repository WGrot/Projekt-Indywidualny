using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_HealthStat : CharacterStat
{

    public delegate void OnHealthModifierAdded();
    public static event OnHealthModifierAdded OnHealthModifierAddedCallback;
    
    public PL_HealthStat(float baseValue) : base(baseValue) {
        base.statType = StatType.Hp;
    }

    public override void AddModifier(StatModifier modifier)
    {
        base.AddModifier(modifier);
        if (OnHealthModifierAddedCallback != null)
        {
            OnHealthModifierAddedCallback();
        }
    }
}
