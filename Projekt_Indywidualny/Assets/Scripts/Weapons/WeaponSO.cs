using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Pool;

public enum WeaponShootingStyle
{
    OneTap,
    FullAuto,
    Charge
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons/Weapon")]
public class WeaponSO : Item
{


    public WeaponShootingStyle shootStyle;
    public WeaponPrefix Prefix;
    public float Damage;
    public float FireRate = 0.25f;
    public float ChargeTime = 0;
    public float ReloadTime;
    public int ClipSize;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.4f);
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public Vector3 ShootOffset;
    public GameObject ModelPrefab;


    private float fireRate = 0.25f;
    private float damage;
    public float chargeTime { get; private set; }
    private float reloadTime;
    private int clipSize;
    private Vector3 spread;

    private float LastShootTime;


    public void OnPickup()
    {
        ResetActualValues();
        ApplyPrefix();
        LastShootTime = Time.time;
    }

    public void ApplyPrefix()
    {
        if (Prefix == null)
        {
            return;
        }
        damage = Damage * (1 + Prefix.damageModifier/100f);
        fireRate = FireRate * (1 + Prefix.fireRateModifier/100f);
        chargeTime = ChargeTime * (1 + Prefix.chargeTimeModifier/100f);
        reloadTime = ReloadTime * (1 + Prefix.reloadTimeModifier/100f);
        spread = Spread * (1 + Prefix.spreadModifier/100f);
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
    
    public void Shoot(GameObject weaponHolder)
    {
        if (Time.time < fireRate + LastShootTime)
        {
            return;
        }
        LastShootTime = Time.time;
        Vector3 direction = weaponHolder.transform.forward;
        direction += new Vector3(
                        Random.Range(-spread.x, spread.x),
                        Random.Range(-spread.y, spread.y),
                        Random.Range(-spread.z, spread.z)
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
    


}
