using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerFire : Projectile
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
        }
    }
    public override bool Parry()
    {
        return false;
    }
}
