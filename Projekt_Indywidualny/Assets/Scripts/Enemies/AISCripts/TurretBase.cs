using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private float fireRate;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float bulletDamage;

    [SerializeField] private float delayAfterSpawn;

    private void OnEnable()
    {
        StartCoroutine("Shoot");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delayAfterSpawn);
        while (true)
        {
            EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint.transform.position, transform.forward, bulletSpeed, bulletLifeTime, bulletDamage);
            yield return new WaitForSeconds(fireRate);
        }

    }
}
