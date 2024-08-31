using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ItemPoolsManager : MonoBehaviour
{
    private List<PassiveItem> allItemsList = new List<PassiveItem>();
    private List<WeaponSO> allWeaponsList = new List<WeaponSO>();
    private List<PassiveItem> unlockedItemsList = new List<PassiveItem>();
    private List<WeaponSO> unlockedWeaponsList = new List<WeaponSO>();

    private ItemPool AllItems = new ItemPool();
    private ItemPool TreasureRoomItems= new ItemPool();




    #region Singleton
    public static ItemPoolsManager Instance { get; private set; }
    public List<WeaponSO> AllWeaponsList { get => allWeaponsList; set => allWeaponsList = value; }
    public List<PassiveItem> AllItemsList { get => allItemsList; set => allItemsList = value; }

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
        allItemsList = PoolAllTMP.ToList();
    }

    public void LoadWeapons()
    {
        UnityEngine.Object[] loadedWeapons = Resources.LoadAll("WeaponsResources/Weapons");
        WeaponSO[] PoolAllTMP = new WeaponSO[loadedWeapons.Length];
        loadedWeapons.CopyTo(PoolAllTMP, 0);
        Array.Sort(PoolAllTMP, new WeaponComparer());
        allWeaponsList = PoolAllTMP.ToList();
    }

    public void AssignItemToPools()
    {
        foreach (PassiveItem item in unlockedItemsList)
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
        foreach (WeaponSO weapon in unlockedWeaponsList)
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
        unlockedItemsList = new List<PassiveItem>(allItemsList);
        for (int i = allItemsList.Count - 1; i >= 0; i--)
        {
            if (!unlockedPassiveItems[i])
            {
                unlockedItemsList.RemoveAt(i);
            }
        }
    }

    public void DeleteLockedWeapons(bool[] unlockedWeapons)
    {
        unlockedWeaponsList = new List<WeaponSO>(allWeaponsList);
        for (int i = allWeaponsList.Count - 1; i >= 0; i--)
        {

            if (!unlockedWeapons[i])
            {
                unlockedWeaponsList.RemoveAt(i);
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
