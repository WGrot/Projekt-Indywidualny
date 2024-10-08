using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndefiniteBuff : Buff
{
    Func<bool> ConditionsFunction;
    public IndefiniteBuff(UnityEngine.Object source, float startTime, Sprite buffIcon, Func<bool> conditionsFunction) : base(0, source, startTime, buffIcon)
    {
        
        this.ConditionsFunction = conditionsFunction;
    }

    public IndefiniteBuff(StatModifier statModifier, UnityEngine.Object source, float startTime, Sprite buffIcon, Func<bool> conditionsFunction) : base(0, statModifier, source, startTime, buffIcon)
    {
        this.ConditionsFunction = conditionsFunction;
    }

    public IndefiniteBuff(List<StatModifier> modifiers, UnityEngine.Object source, float startTime, Sprite buffIcon, Func<bool> conditionsFunction) : base(0, modifiers, source, startTime, buffIcon)
    {
        this.ConditionsFunction = conditionsFunction;
    }




    public override bool AreConditionsMet()
    {
        return ConditionsFunction();
    }

}
