using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/PainGun")]
public class PainGunBH : WeaponBehaviour
{
    public float slowTimeDuration = 2;
    public float slowTimePower = 5;
    public override void OnShoot()
    {
        GameStateManager.Instance.StartSlowTime(slowTimePower, slowTimeDuration);
    }
}
