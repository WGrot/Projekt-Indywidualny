using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public enum WeaponShootingStyle
{
    OneTap,
    FullAuto,
    Charge
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons/Weapon")]
public class WeaponSO : Item
{
    [Header("Weapon Stats")]
    public WeaponShootingStyle shootStyle;
    public float Damage;
    public float FireRate = 0.25f;
    public float ChargeTime = 0f;
    public float ReloadTime = 2f;
    public int ClipSize = 10;
    public int MaxAmmo = 100;
    public int AmmoUsePerShoot = 1;
    public int bulletsPerShoot = 1;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.4f);


    [Header("Weapon Model Parameters")]
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public Vector3 ModelScale = new Vector3(1f, 1f, 1f);
    public Vector3 ShootOffset;
    public GameObject ModelPrefab;
    public Sprite idleImage;


    private float fireRate = 0.25f;
    private float damage;
    public float chargeTime { get; private set; }
    public float reloadTime { get; private set; }
    private Vector3 spread;

    private float LastShootTime;


    public void OnPickup()
    {
        ResetActualValues();
        LastShootTime = Time.time;
    }

    public void OnEquip()
    {
        ResetActualValues();
    }

    public void ApplyPrefix(WeaponPrefix prefix)
    {
        if (prefix == null)
        {
            return;
        }
        ResetActualValues();
        damage = Damage * (1 + prefix.damageModifier/100f);
        fireRate = FireRate * (1 + prefix.fireRateModifier/100f);
        chargeTime = ChargeTime * (1 + prefix.chargeTimeModifier/100f);
        reloadTime = ReloadTime * (1 + prefix.reloadTimeModifier/100f);
        spread = Spread * (1 + prefix.spreadModifier/100f);
    }

    public void ResetActualValues()
    {
        damage = Damage;
        fireRate = FireRate;
        chargeTime = ChargeTime;
        reloadTime = ReloadTime;
        spread = Spread;
    }

    
    public void Shoot(GameObject weaponHolder, int activeWeaponId)
    {
        if (Time.time < fireRate + LastShootTime)
        {
            return;
        }
        LastShootTime = Time.time;
        Inventory.Instance.DecreaseAmmoAtIndex(activeWeaponId, AmmoUsePerShoot);
        for (int i = 0; i < bulletsPerShoot; i++)
        {


            Vector3 direction = weaponHolder.transform.forward;
            direction += new Vector3(
                            UnityEngine.Random.Range(-spread.x, spread.x),
                            UnityEngine.Random.Range(-spread.y, spread.y),
                            UnityEngine.Random.Range(-spread.z, spread.z)
                        );
            if (Physics.Raycast(weaponHolder.transform.position + ShootOffset, direction, out RaycastHit hit))
            {
                Instantiate(ModelPrefab, hit.point + hit.normal * 0.05f, Quaternion.LookRotation(hit.normal));
                Ihp target = hit.transform.GetComponent<Ihp>();
                if (target != null)
                {
                    target.TakeDamage(damage * PlayerStatus.Instance.GetCharacterStatValueOfType(StatType.Damage));
                }
            }
        }

    }




    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        WeaponSO other = (WeaponSO)obj;
        return itemName == other.itemName;
    }

    public override int GetHashCode()
    {
        return itemName.GetHashCode();
    }

}
