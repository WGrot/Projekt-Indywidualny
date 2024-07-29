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
    public ShootConfig shootConfiguration = null;
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

    private WeaponBehaviour weaponBehaviour;

    [Header("Weapon Model Parameters")]
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public Vector3 ModelScale = new Vector3(1f, 1f, 1f);
    public Vector3 ShootOffset;
    public GameObject ModelPrefab;
    public Sprite idleImage;

    [Header("Weapon Model Parameters")]
    public AudioClip weaponShootSound;
    public AudioClip weaponReloadSound;

    private float fireRate = 0.25f;
    private float damage;
    public float chargeTime { get; private set; }
    public float reloadTime { get; private set; }
    public WeaponBehaviour WeaponBehaviour { get => weaponBehaviour; private set => weaponBehaviour = value; }

    private Vector3 spread;

    private float LastShootTime;


    public void OnPickup()
    {
        ResetActualValues();
        LastShootTime = Time.time;
        if(itemBehaviour != null && itemBehaviour is WeaponBehaviour)
        {
            weaponBehaviour= (WeaponBehaviour)itemBehaviour;
        }
    }

    public void OnFirstEquip()
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

    
    public bool Shoot(GameObject weaponHolder, int activeWeaponId)
    {
        if (Time.time < fireRate + LastShootTime)
        {
            return false;
        }
        LastShootTime = Time.time;
        Inventory.Instance.DecreaseAmmoAtIndex(activeWeaponId, AmmoUsePerShoot);
        for (int i = 0; i < bulletsPerShoot; i++)
        {
            if(shootConfiguration != null)
            {
                //0_0 
                shootConfiguration.Shoot(weaponHolder,activeWeaponId, damage, fireRate, LastShootTime, AmmoUsePerShoot, bulletsPerShoot, spread, ShootOffset, ModelPrefab);
            }
            else
            {
                Vector3 direction = weaponHolder.transform.forward;
                direction += new Vector3(
                                UnityEngine.Random.Range(-spread.x, spread.x),
                                UnityEngine.Random.Range(-spread.y, spread.y),
                                UnityEngine.Random.Range(-spread.z, spread.z)
                            );
                if (Physics.Raycast(weaponHolder.transform.position + ShootOffset, direction, out RaycastHit hit))
                {
                    Debug.DrawLine(weaponHolder.transform.position + ShootOffset, hit.point, Color.green, 2.5f);
                    Instantiate(ModelPrefab, hit.point + hit.normal * 0.05f, Quaternion.LookRotation(hit.normal));
                    if (hit.transform.TryGetComponent<Ihp>(out var target))
                    {
                        target.TakeDamage(damage * PlayerStatus.Instance.GetCharacterStatValueOfType(StatType.Damage));
                    }
                }
            }
        }

        if (weaponBehaviour != null)
        {
            weaponBehaviour.OnShoot();
        }

        return true;

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
