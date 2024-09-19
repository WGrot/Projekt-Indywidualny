using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAtStartData
{
    public bool[] unlockedItems;
    public bool[] unlockedWeapons;

    public UnlockAtStartData(bool[] unlockedItems, bool[] unlockedWeapons)
    {
        this.unlockedItems = unlockedItems;
        this.unlockedWeapons = unlockedWeapons;
    }
}

