using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons/Weapon Prefix")]
public class WeaponPrefix : ScriptableObject
{
    public string PrefixName = "New Prefix";

    public WeaponShootingStyle forWhatType;
    public int damageModifier;
    public int fireRateModifier;
    public int chargeTimeModifier;
    public int spreadModifier;
    public int reloadTimeModifier;
}
