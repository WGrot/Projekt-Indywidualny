using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedProjectile : Projectile
{
    private float chargePower;

    public float ChargePower { get => chargePower; set => chargePower = value; }
}
