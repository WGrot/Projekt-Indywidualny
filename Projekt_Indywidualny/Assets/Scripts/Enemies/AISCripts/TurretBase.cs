using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private float fireRate;

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
        while (true)
        {
            Instantiate(bulletPrefab, shootPoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(fireRate);
        }

    }
}
