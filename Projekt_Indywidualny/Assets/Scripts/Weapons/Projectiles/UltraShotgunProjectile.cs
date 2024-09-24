using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraShotgunProjectile : Projectile
{
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity += transform.forward * speed;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Ihp>(out var enemyHp) && other.CompareTag("Enemy"))
        {
            enemyHp.TakeDamage(damage * PlayerStatus.Instance.GetCharacterStatValueOfType(StatType.Damage));
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);

        }
    }

    public override bool Parry()
    {
        Vector3 direction = PlayerReferences.Instance.LookPoint.transform.forward;
        float parriedSpeed = ConstantValues.PARRIED_PROJECTILE_SPEED;
        ParriedProjectilesObjectPool.Instance.ShootBullet(transform.position, direction, parriedSpeed, lifeTime, 2* damage);
        base.Parry();
        Destroy(gameObject);
        return true;
    }
}
