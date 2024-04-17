using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBasic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    private Rigidbody rb;
    private TrailRenderer trailRenderer;

    public delegate void OnDisableCallback(EnemyProjectileBasic instance);
    public event OnDisableCallback OnDisable;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    public void Shoot(Vector3 position, Vector3 direction, float newSpeed, float newLifeTime, float newDamage)
    {

        transform.position = position;
        transform.forward= direction;
        trailRenderer.Clear();
        speed = newSpeed;
        lifeTime = newLifeTime;
        damage = newDamage;

        rb.velocity = transform.forward * speed;


    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    public void SetLifeTime(int newLifeTime)
    {
        lifeTime = newLifeTime;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            OnDisable?.Invoke(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.Instance.TakeDamage(damage);
            OnDisable?.Invoke(this);
        }
    }

}
