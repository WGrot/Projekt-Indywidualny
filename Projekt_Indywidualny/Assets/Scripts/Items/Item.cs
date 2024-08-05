using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public string description = "put description here";
    public ItemPools[] Pools = {ItemPools.All};
    public Rarities Rarity = Rarities.Common;
    public Sprite icon = null;
    public ItemBehaviour itemBehaviour;

    public void AddBehavioursToManager()
    {
        if (itemBehaviour == null)
        {
            return;
        }
        Inventory.Instance.ItemBehaviourManager.AddFuncToOnPlayerTakeDamageBH(itemBehaviour.OnPlayerTakeDamage);
        Inventory.Instance.ItemBehaviourManager.AddFuncToOnEnemyDeath(itemBehaviour.OnEnemyDeath);
    }

    public void RemoveBehavioursFromManager()
    {
        if (itemBehaviour == null)
        {
            return;
        }
        Inventory.Instance.ItemBehaviourManager.RemoveFuncFromOnPlayerTakeDamageBH(itemBehaviour.OnPlayerTakeDamage);
        Inventory.Instance.ItemBehaviourManager.RemoveFuncFromOnEnemyDeath(itemBehaviour.OnEnemyDeath);
    }
}
