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

    public delegate void OnItemAdded(Item item);
    public static event OnItemAdded OnItemAddedCallback;
    //public delegate void OnItemRemoved(Item item);
    //public static event OnItemRemoved OnItemRemovedCallback;
    public delegate void OnItemWeapon();
    public static event OnItemWeapon OnWeaponAddedCallback;
    public delegate void OnAmmoRefiled(int activeweaponID);
    public static event OnAmmoRefiled OnAmmoRefiledCallback;

    private List<PassiveItem> passiveItems = new List<PassiveItem>();
    private List<WeaponSO> weapons = new List<WeaponSO>();
    private List<WeaponPrefix> prefixes= new List<WeaponPrefix>();
    private List<AmmoData> ammos = new List<AmmoData>();

    private int activeWeaponID = 0;

    public ItemBehaviourManager itemBehaviourManager { get; private set; } = new ItemBehaviourManager();

    public void SetActiveWeaponID(int id)
    {
        activeWeaponID = id;
    }

    public void Start()
    {
        itemBehaviourManager.ClearAllBehaviours();
        ItemBehaviourManager.ResetAllBehaviours();
    }

    private void OnDisable()
    {
        itemBehaviourManager.ClearAllBehaviours();
        itemBehaviourManager.UnSubToAllEvents();
    }
    public int GetActiveWeaponID()
    {
        return activeWeaponID;
    }

    public void RefillActiveWeaponAmmoByPercent(int percent)
    {
        if(weapons.Count == 0)
        {
            return;
        }
        ammos[activeWeaponID].RefillByPercent(percent);
        if (OnAmmoRefiledCallback!= null)
        {
            OnAmmoRefiledCallback(activeWeaponID);
        }
    }

    #region PassiveItems

    public List<PassiveItem> GetPassiveItemList()
    {
        return passiveItems;
    }

    public void AddPassiveItem(PassiveItem item)
    {
        passiveItems.Add(item);
        item.ApplyModifiersOnPickUp();
        item.AddBehavioursToManager();
        OnItemAddedCallback?.Invoke(item);
    }

    public bool RemovePassiveItem(PassiveItem item)
    {
        item.RemoveModifiersOnDrop();
        if(item.itemBehaviour != null)
        {
            item.itemBehaviour.OnDrop();
        }
        item.RemoveBehavioursFromManager();
        return passiveItems.Remove(item);
    }
    #endregion


    #region WeaponsAndPrefixes
    public List<WeaponSO> GetWeaponsList()
    {
        return weapons;
    }

    public WeaponSO GetWeaponAtIndex(int index)
    {
        return weapons[index];
    }

    public WeaponSO GetActiveWeapon()
    {
        return weapons[activeWeaponID];
    }

    public void AddWeapon(WeaponSO weapon)
    {
        weapons.Add(weapon);
        weapon.OnPickup();
        //weapon.AddBehavioursToManager();
        OnWeaponAddedCallback?.Invoke();
    }

    public bool RemoveWeapon(WeaponSO weapon)
    {
        weapon.RemoveBehavioursFromManager();
        if (weapon.itemBehaviour != null)
        {
            weapon.itemBehaviour.OnDrop();
        }
        return weapons.Remove(weapon);
    }

    public void RemoveWeaponAtIndex(int index)
    {
        weapons[index].RemoveBehavioursFromManager();
        if (weapons[index].itemBehaviour != null)
        {
            weapons[index].itemBehaviour.OnDrop();
        }
        weapons.RemoveAt(index);
    }

    public bool IsWeaponAlreadyInList(WeaponSO weapon)
    {
        return weapons.Contains(weapon);
    }

    public int GetWeaponIndex(WeaponSO weapon)
    {
        return weapons.IndexOf(weapon);
    }




    public void AddPrefix(WeaponPrefix prefix)
    {
        prefixes.Add(prefix);
    }

    public List<WeaponPrefix> GetPrefixList()
    {
        return prefixes;
    }
    public bool RemovePrefix(WeaponPrefix prefix)
    {
        return prefixes.Remove(prefix);
    }
    public void RemovePrefixAtIndex(int index)
    {
        prefixes.RemoveAt(index);
    }

    public WeaponPrefix GetPrefixWithIndex(int index)
    {
        return prefixes[index];
    }







    public List<AmmoData> GetAmmoList()
    {
        return ammos;
    }
    public void AddAmmo(AmmoData ammoData)
    {
         ammos.Add(ammoData);
    }
    public bool RemoveAmmo(AmmoData ammoData)
    {

        return ammos.Remove(ammoData);
    }
    public void RemoveAmmoAtIndex(int index)
    {
        ammos.RemoveAt(index);
    }

    public AmmoData GetAmmoAtIndex(int index)
    {
        return ammos[index];
    }

    public AmmoData GetAmmoOfActiveWeapon()
    {
        return ammos[activeWeaponID];
    }
    public void DecreaseAmmoAtIndex(int index, int amount)
    {
        ammos[index].DecreaseAmmo(amount);
    }
    #endregion
}
