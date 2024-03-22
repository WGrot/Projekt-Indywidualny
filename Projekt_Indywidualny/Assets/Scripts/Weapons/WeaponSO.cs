using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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
    public float ChargeTime = 0;
    public float ReloadTime;
    public int ClipSize;
    public int MaxAmmo;
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
    private float reloadTime;
    private int clipSize;
    private Vector3 spread;
    private int ammoLeft;

    private float LastShootTime;
    [NonSerialized] private bool isFirstPickup = true;



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
        Debug.Log("applied Prefixes");
    }

    public void ResetActualValues()
    {
        damage = Damage;
        fireRate = FireRate;
        chargeTime = ChargeTime;
        reloadTime = ReloadTime;
        spread = Spread;
    }

    public void ResetAmmoIfNeeded()
    {
        if (isFirstPickup)
        {
            ammoLeft = MaxAmmo;
            isFirstPickup= false;
        }
    }
    
    public void Shoot(GameObject weaponHolder)
    {
        if (Time.time < fireRate + LastShootTime)
        {
            return;
        }
        LastShootTime = Time.time;
        Vector3 direction = weaponHolder.transform.forward;
        direction += new Vector3(
                        UnityEngine.Random.Range(-spread.x, spread.x),
                        UnityEngine.Random.Range(-spread.y, spread.y),
                        UnityEngine.Random.Range(-spread.z, spread.z)
                    );
        if (Physics.Raycast(weaponHolder.transform.position + ShootOffset, direction, out RaycastHit hit))
        {
            Instantiate(ModelPrefab, hit.point, Quaternion.identity);
            Ihp target = hit.transform.GetComponent<Ihp>();
            if (target != null)
            {
                target.TakeDamage(damage);
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
