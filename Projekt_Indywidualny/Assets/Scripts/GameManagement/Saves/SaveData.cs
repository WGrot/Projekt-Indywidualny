using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData {

    public bool[] UnlockedPassiveItemsData = new bool[ItemPoolsManager.Instance.PoolAll.Count];
    public bool[] UnlockedWeaponsData = new bool[ItemPoolsManager.Instance.PoolAll.Count];


    public void UnlockItem(int id)
    {
        UnlockedPassiveItemsData[id] = true;
    }

    public void UnlockWeapon(int id)
    {
        UnlockedPassiveItemsData[id] = true;
    }
}
