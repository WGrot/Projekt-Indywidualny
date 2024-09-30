using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootConfig : ScriptableObject
{
    public abstract void Shoot(GameObject weaponHolder, int activeWeaponId, float damage, float fireRate, float LastShootTime, int AmmoUsePerShoot, int bulletsPerShoot, Vector3 spread, Vector3 ShootOffset, GameObject ModelPrefab, float chargePower);
    
}
