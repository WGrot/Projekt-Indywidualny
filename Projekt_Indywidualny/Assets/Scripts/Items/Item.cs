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
    public bool UnlockedAtStart = true;

    public void AddBehavioursToManager()
    {
        if (itemBehaviour == null)
        {
            return;
        }
        Debug.Log("Added Behaviours from " + name);
        Inventory.Instance.itemBehaviourManager.AddFuncToOnPlayerTakeDamageBH(itemBehaviour.OnPlayerTakeDamage);
        Inventory.Instance.itemBehaviourManager.AddFuncToOnEnemyDeath(itemBehaviour.OnEnemyDeath);
        Inventory.Instance.itemBehaviourManager.AddFuncToOnCoinAmountChange(itemBehaviour.OnCoinAmountChange);
        Inventory.Instance.itemBehaviourManager.AddFuncToOnLevelGenrated(itemBehaviour.OnLevelGenerated);
    }

    public void RemoveBehavioursFromManager()
    {
        if (itemBehaviour == null)
        {
            return;
        }
        Debug.Log("Removed Behaviours from " + name);
        Inventory.Instance.itemBehaviourManager.RemoveFuncFromOnPlayerTakeDamageBH(itemBehaviour.OnPlayerTakeDamage);
        Inventory.Instance.itemBehaviourManager.RemoveFuncFromOnEnemyDeath(itemBehaviour.OnEnemyDeath);
        Inventory.Instance.itemBehaviourManager.RemoveFuncFromOnCoinAmountChange(itemBehaviour.OnCoinAmountChange);
        Inventory.Instance.itemBehaviourManager.RemoveFuncFromOnLevelGenerated(itemBehaviour.OnLevelGenerated);
    }
}
