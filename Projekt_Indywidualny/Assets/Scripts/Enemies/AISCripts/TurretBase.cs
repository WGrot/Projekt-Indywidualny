using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<GameObject> shootPoint;
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
            for (int i = 0; i < shootPoint.Count; i++)
            {
                EnemyProjectileObjectPool.Instance.ShootBullet(shootPoint[i].transform.position, shootPoint[i].transform.forward, bulletSpeed, bulletLifeTime, bulletDamage);

            }
            yield return new WaitForSeconds(fireRate);
        }

    }
}
