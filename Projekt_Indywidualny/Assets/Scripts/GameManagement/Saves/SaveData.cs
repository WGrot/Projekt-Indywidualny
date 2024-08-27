using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData {

    public bool[] UnlockedPassiveItemsData = new bool[10];
    public bool[] UnlockedWeaponsData = new bool[10];

    public SaveData(bool[] items, bool[] weapons)
    {
        this.UnlockedPassiveItemsData = items;
        this.UnlockedWeaponsData = weapons;
    }

    public void UnlockItem(int id)
    {
        UnlockedPassiveItemsData[id] = true;
    }

    public void UnlockWeapon(int id)
    {
        UnlockedPassiveItemsData[id] = true;
    }
}
