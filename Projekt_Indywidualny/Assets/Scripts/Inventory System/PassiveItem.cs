using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Passive Item")]
public class PassiveItem : Item
{
    public List<StatModifier> modifiers;

    public void OnPickUp()
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

    public void OnDrop()
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
