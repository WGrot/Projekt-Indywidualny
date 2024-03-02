using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMulti = 300
}

public class StatModifier
{
    public readonly float value;
    public readonly StatModType modType;
    public readonly int order;
    public readonly object source;

    public StatModifier(float value, StatModType modType, int order, object source)
    {
        this.value = value;
        this.modType = modType;
        this.order = order;
        this.source = source;
    }
    public StatModifier(float value, StatModType modType)
    {
        this.value = value;
        this.modType = modType;
        this.order = (int)modType;
        this.source = null;
    }

    public StatModifier(float value, StatModType modType, int order)
    {
        this.value = value;
        this.modType = modType;
        this.order = order;
        this.source = null;
    }

    public StatModifier(float value, StatModType modType, object source)
    {
        this.value = value;
        this.modType = modType;
        this.order = (int)modType;
        this.source = null;
    }
}
