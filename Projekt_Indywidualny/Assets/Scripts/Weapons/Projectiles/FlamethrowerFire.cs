using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerFire : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    private Rigidbody rb;


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
}
