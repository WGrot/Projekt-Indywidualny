using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one Inventory instance found!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public static event OnItemChanged OnItemChangedCallback;
    public delegate void OnItemWeapon();
    public static event OnItemWeapon OnWeaponChangedCallback;
    public static event OnItemWeapon OnWeaponAddedCallback;

    private List<PassiveItem> passiveItems = new List<PassiveItem>();
    private List<WeaponSO> weapons = new List<WeaponSO>();
    private List<WeaponPrefix> prefixes= new List<WeaponPrefix>();

    #region PassiveItems

    public List<PassiveItem> GetPassiveItemList()
    {
        return passiveItems;
    }

    public void AddPassiveItem(PassiveItem item)
    {
        passiveItems.Add(item);
        item.OnPickUp();
        OnItemChangedCallback?.Invoke();
    }

    public bool RemovePassiveItem(PassiveItem item)
    {
        item.OnDrop();
        OnItemChangedCallback?.Invoke();
        return passiveItems.Remove(item);
    }
    #endregion



    public List<WeaponSO> GetWeaponsList()
    {
        return weapons;
    }

    public WeaponSO GetWeaponWithIndex(int index)
    {
        return weapons[index];
    }

    public void AddWeapon(WeaponSO weapon)
    {
        weapons.Add(weapon);
        weapon.OnPickup();
        OnWeaponAddedCallback?.Invoke();
    }

    public void AddPrefix(WeaponPrefix prefix)
    {
        prefixes.Add(prefix);
    }

    public List<WeaponPrefix> GetPrefixList()
    {
        return prefixes;
    }

    public bool RemoveWeapon(WeaponSO weapon)
    {
        
        return weapons.Remove(weapon);
    }

    public bool RemovePrefix(WeaponPrefix prefix)
    {
        return prefixes.Remove(prefix);
    }

    public WeaponPrefix GetPrefixWithIndex(int index)
    {
        return prefixes[index];
    }

}
