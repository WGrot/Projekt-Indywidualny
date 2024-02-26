using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public int x;
    public int z;
    public GridNode prev;

    public GridNode(int x, int z, GridNode prev)
    {
        this.x = x;
        this.z = z;
        this.prev = prev;
    }

    public override bool Equals(object obj)
    {
        return obj is GridNode node &&
               x == node.x &&
               z == node.z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z);
    }
}
