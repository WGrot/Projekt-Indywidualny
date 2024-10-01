using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ChargedWeapon Behaviour")]
public class ChargedWeaponBehaviour : WeaponBehaviour
{
    public float ShakeIntesivityDuringCharging;
    public float ShakeTimeDuringCharging;

    public virtual void OnCharging(float chargedTime, float completeChargeTime)
    {
        CameraShake.Instance.ShakeCamera(ShakeIntesivityDuringCharging * (chargedTime/completeChargeTime), ShakeTimeDuringCharging);

    }
}
