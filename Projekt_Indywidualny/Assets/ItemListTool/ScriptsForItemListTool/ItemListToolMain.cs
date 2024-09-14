using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListToolMain : MonoBehaviour
{
    [SerializeField] GameObject ItemEntry;
    [SerializeField] GameObject ItemList;
    [SerializeField] GameObject WeaponList;
    List<Itementry> entries= new List<Itementry>();
    List<Itementry> weaponEntries = new List<Itementry>();
    public void LoadItems()
    {
        entries.Clear();
        foreach (Transform child in ItemList.transform)
        {
            Destroy(child.gameObject);
        }

        List<PassiveItem> items= new List<PassiveItem>();
        items = ItemPoolsManager.Instance.AllItemsList;
        foreach (PassiveItem item in items)
        {

            GameObject entry = Instantiate(ItemEntry, ItemList.transform);
            Itementry script = entry.GetComponent<Itementry>();
            script.LoadItem(item);
            entries.Add(script);
        }
    }

    public void LoadWeapons()
    {
        weaponEntries.Clear();
        foreach (Transform child in WeaponList.transform)
        {
            Destroy(child.gameObject);
        }

        List<WeaponSO> weapons = new List<WeaponSO>();
        weapons = ItemPoolsManager.Instance.AllWeaponsList;
        foreach (WeaponSO weapon in weapons)
        {
            Debug.Log(weapon.name);
            GameObject entry = Instantiate(ItemEntry, WeaponList.transform);
            Itementry script = entry.GetComponent<Itementry>();
            script.LoadItem(weapon);
            weaponEntries.Add(script);
        }
    }


    public void SaveChanges()
    {
        bool[] unlockedatStartItems = new bool[entries.Count];
        bool[] unlockedatStartWeapons = new bool[weaponEntries.Count];

        for (int i = 0; i < entries.Count;i++)
        {
            Itementry entry = entries[i];
            entry.Save();
            unlockedatStartItems[i] = entry.unlockedAtStart.isOn;
        }


        for (int i = 0; i < weaponEntries.Count; i++)
        {
            Itementry entry = weaponEntries[i];
            entry.Save();
            unlockedatStartWeapons[i] = entry.unlockedAtStart.isOn;
        }


        SaveManager.Instance.CreateUnlockAtStartFile(unlockedatStartItems, unlockedatStartWeapons);
        SaveManager.Instance.ResetSave();
        /*
        foreach (Itementry entry in entries)
        {
            entry.Save();

        }

        foreach (Itementry entry in weaponEntries)
        {
            entry.Save();
        }
        */
    }


}
