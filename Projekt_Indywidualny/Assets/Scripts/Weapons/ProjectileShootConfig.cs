using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons/ProjectileShootConfiguration")]
public class ProjectileShootConfig : ShootConfig
{
    public GameObject WeaponProjectile;
    public bool shouldAccountPlayerSpeed;
    public override void Shoot(GameObject weaponHolder, int activeWeaponId, float damage, float fireRate, float LastShootTime, int AmmoUsePerShoot, int bulletsPerShoot, Vector3 spread, Vector3 ShootOffset, GameObject ModelPrefab)
    {
        Vector3 direction = weaponHolder.transform.forward;
        direction += new Vector3(
                        UnityEngine.Random.Range(-spread.x, spread.x),
                        UnityEngine.Random.Range(-spread.y, spread.y),
                        UnityEngine.Random.Range(-spread.z, spread.z)
                    );
        if (Physics.Raycast(weaponHolder.transform.position, direction, out RaycastHit hit))
        {
            direction = (hit.point - (weaponHolder.transform.position/* + ShootOffset*/)).normalized;
        }

        GameObject projectile = Instantiate(WeaponProjectile, weaponHolder.transform.position /*+ weaponHolder.transform.rotation * ShootOffset*/, weaponHolder.transform.rotation);
        projectile.transform.forward = direction;
        if (shouldAccountPlayerSpeed)
        {
            Vector3 playerVelocity = PlayerStatus.Instance.GetPlayerBody().GetComponent<CharacterController>().velocity;
            projectile.GetComponent<Rigidbody>().velocity += playerVelocity;
        }

    }
}
