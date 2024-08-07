using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComparer : IComparer<WeaponSO>
{
    public int Compare(WeaponSO x, WeaponSO y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentException("Obiekty do porównania nie mog¹ byæ null");
        }
        return x.WeaponID.CompareTo(y.WeaponID);
    }

}
