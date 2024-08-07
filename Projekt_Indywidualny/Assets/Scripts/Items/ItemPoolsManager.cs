using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemPoolsManager : MonoBehaviour
{
    private List<PassiveItem> AllItemsList = new List<PassiveItem>();
    private List<WeaponSO> AllWeaponsList = new List<WeaponSO>();
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
        AllItemsList = PoolAllTMP.ToList();
    }

    public void LoadWeapons()
    {
        UnityEngine.Object[] loadedWeapons = Resources.LoadAll("WeaponsResources/Weapons");
        WeaponSO[] PoolAllTMP = new WeaponSO[loadedWeapons.Length];
        loadedWeapons.CopyTo(PoolAllTMP, 0);
        Array.Sort(PoolAllTMP, new WeaponComparer());
        AllWeaponsList = PoolAllTMP.ToList();
    }

    public void AssignItemToPools()
    {
        foreach (PassiveItem item in AllItemsList)
        {
            foreach (ItemPools pool in item.Pools)
            {
                switch (pool)
                {
                    case ItemPools.All:
                        AllItems.AddItemToCorrectList(item);
                        break;
                    case ItemPools.TreasureRoom:
                        TreasureRoomItems.AddItemToCorrectList(item);
                        break;
                }
            }
        }
    }

    public void AssignWeaponsToPools()
    {
        foreach (WeaponSO weapon in AllWeaponsList)
        {
            foreach (ItemPools pool in weapon.Pools)
            {
                switch (pool)
                {
                    case ItemPools.All:
                        AllItems.AddItemToCorrectList(weapon);
                        break;
                    case ItemPools.TreasureRoom:
                        TreasureRoomItems.AddItemToCorrectList(weapon);
                        break;
                }
            }
        }
    }


    public void DeleteLockedItems(bool[] unlockedPassiveItems)
    {
        for (int i = AllItemsList.Count - 1; i >= 0; i--)
        {
            if (!unlockedPassiveItems[i])
            {
                AllItemsList.RemoveAt(i);
            }
        }
    }

    public void DeleteLockedWeapons(bool[] unlockedWeapons)
    {
        for (int i = AllItemsList.Count - 1; i >= 0; i--)
        {
            if (!unlockedWeapons[i])
            {
                AllWeaponsList.RemoveAt(i);
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


    public WeaponSO GetRandomWeaponFromPool(ItemPools pool)
    {
        WeaponSO result = null;
        switch (pool)
        {
            case ItemPools.All:
                result = AllItems.GetRandomWeapon();
                break;
            case ItemPools.TreasureRoom:
                result = TreasureRoomItems.GetRandomWeapon();
                break;
        }
        return result;
    }
}
