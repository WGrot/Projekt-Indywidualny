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
    public float FireRate = 0.25f;
    public float damage;
    public float chargeTime = 0;

    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);

    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;
    public Vector3 ShootOffset;
    public GameObject ModelPrefab;

    private float LastShootTime;


    public void OnPickup()
    {
        Debug.Log("podniesiono cuœ");
        LastShootTime = Time.time;
    }

    public void Shoot(GameObject weaponHolder)
    {
        if (Time.time < FireRate + LastShootTime)
        {
            return;
        }
        LastShootTime = Time.time;
        Vector3 direction = weaponHolder.transform.forward;
        direction += new Vector3(
                        Random.Range(-Spread.x, Spread.x),
                        Random.Range(-Spread.y, Spread.y),
                        Random.Range(-Spread.z, Spread.z)
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
