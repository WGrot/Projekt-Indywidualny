using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemPoolsManager : MonoBehaviour
{
    private List<PassiveItem> PoolAll = new List<PassiveItem>();
    private ItemPool AllItems = new ItemPool();
    private ItemPool TreasureRoomItems= new ItemPool();


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
    public void LoadPassiveItems()
    {
        UnityEngine.Object[] loadedItems = Resources.LoadAll("ItemsResources/Items/PassiveItems");
        PassiveItem[] PoolAllTMP = new PassiveItem[loadedItems.Length];
        loadedItems.CopyTo(PoolAllTMP, 0);
        Array.Sort(PoolAllTMP, new PassiveItemComparer());
        PoolAll = PoolAllTMP.ToList();
    }

    public void AssignItemToPools()
    {
        foreach (PassiveItem item in PoolAll)
        {
            foreach (ItemPools pool in item.Pools)
            {
                switch (pool)
                {
                    case ItemPools.All:
                        AllItems.AddItemToCorrectListList(item);
                        break;
                    case ItemPools.TreasureRoom:
                        TreasureRoomItems.AddItemToCorrectListList(item);
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

    public PassiveItem GetRandomPassiveItemFromPool(ItemPools pool)
    {
        PassiveItem result = null;
        switch (pool)
        {
            case ItemPools.All:
                result = AllItems.GetRandomPassiveItem();
                break;
            case ItemPools.TreasureRoom:
                result = TreasureRoomItems.GetRandomPassiveItem();
                break;
        }
        return result;
    }
}
