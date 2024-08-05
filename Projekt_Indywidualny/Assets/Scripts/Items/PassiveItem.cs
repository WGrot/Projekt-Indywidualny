using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Passive Item")]
public class PassiveItem : Item
{
    public int PassiveItemID;

    public List<StatModifier> modifiers;

    public void ApplyModifiersOnPickUp()
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            StatModifier modifier = modifiers[i];
            int numberOfStats = PlayerStatus.Instance.stats.Count;
            for (int j = 0; j < numberOfStats; j++)
            {
                CharacterStat stat = PlayerStatus.Instance.stats[j];
                if (modifier.statType == stat.statType)
                {
                    stat.AddModifier(modifier);
                }
            }
        }
       
    }

    public void RemoveModifiersOnDrop()
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            StatModifier modifier = modifiers[i];
            int numberOfStats = PlayerStatus.Instance.stats.Count;
            for (int j = 0; j < numberOfStats; j++)
            {
                CharacterStat stat = PlayerStatus.Instance.stats[j];
                if (modifier.statType == stat.statType)
                {
                    stat.RemoveModifier(modifier);
                }
            }
        }
    }

}
