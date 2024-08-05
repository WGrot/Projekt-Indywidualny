using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItemComparer : IComparer<PassiveItem>
{
    public int Compare(PassiveItem x, PassiveItem y)
    {
        if (x == null || y == null)
        {
            throw new ArgumentException("Obiekty do por�wnania nie mog� by� null");
        }
        return x.PassiveItemID.CompareTo(y.PassiveItemID);
    }
}
