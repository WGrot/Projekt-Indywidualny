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
            throw new ArgumentException("Obiekty do por�wnania nie mog� by� null");
        }
        return x.WeaponID.CompareTo(y.WeaponID);
    }

}
