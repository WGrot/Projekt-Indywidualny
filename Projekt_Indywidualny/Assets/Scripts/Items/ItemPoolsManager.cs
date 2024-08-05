using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPoolsManager : MonoBehaviour
{
    public List<PassiveItem> PoolAll = new List<PassiveItem>();
    public List<PassiveItem> PoolTreasureRoom = new List<PassiveItem>();

    #region Singleton
    public static ItemPoolsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one ItemPoolManager instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    public void LoadPools()
    {
        UnityEngine.Object[] loadedItems = Resources.LoadAll("ItemsResources/Items/PassiveItems");
        PassiveItem[] PoolAllTMP = new PassiveItem[loadedItems.Length];
        loadedItems.CopyTo(PoolAllTMP, 0);
        Array.Sort(PoolAllTMP, new PassiveItemComparer());
        PoolAll = PoolAllTMP.ToList();


        foreach (PassiveItem item in PoolAllTMP)
        {
            foreach (ItemPools pool in item.Pools)
            {
                switch (pool)
                {
                    case ItemPools.TreasureRoom:
                        PoolTreasureRoom.Add(item);
                        break;
                }
            }
        }
    }

    public void DeleteLockedItems(bool[] unlockedPassiveItems)
    {
        for (int i = PoolAll.Count - 1; i >= 0; i--)
        {
            if (!unlockedPassiveItems[i])
            {
                PoolAll.RemoveAt(i);
            }
        }
    }
}
