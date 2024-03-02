using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    [SerializeField] private float baseValue;

    public float value { get { return CalculateFinalValue(); } }    //Mo¿na coœ wymyœliæ ¿eby nie obliczaæ tego za ka¿dym razem

    private readonly List<StatModifier> statModifiers;


    public CharacterStat(float baseValue)
    {
        statModifiers = new List<StatModifier>();
        this.baseValue = baseValue;
    }

    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier modifier)
    {
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifierOrder);
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }

    public bool RemoveModifier(StatModifier modifier)
    {
        return statModifiers.Remove(modifier);
    }

    public bool RemoveAllModsFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i > 0; i--)
        {
            if (statModifiers[i].source == source)
            {
                statModifiers.RemoveAt(i);
                didRemove = true;
            }
        }
        return didRemove;
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier modifier = statModifiers[i];
            if (modifier.modType == StatModType.Flat)
            {
                finalValue += statModifiers[i].value;
            }
            else if (modifier.modType == StatModType.PercentAdd)
            {
                sumPercentAdd += modifier.value;

                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].modType != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (modifier.modType == StatModType.PercentMulti)
            {
                finalValue *= 1 + modifier.value;
            }


        }

        return (float)Math.Round(finalValue,4);
    }
}
