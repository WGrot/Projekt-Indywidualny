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
            throw new ArgumentException("Obiekty do porównania nie mog¹ byæ null");
        }
        return x.PassiveItemID.CompareTo(y.PassiveItemID);
    }
}
