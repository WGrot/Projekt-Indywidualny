using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class EnemyProjectileBasic : Projectile
{
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    public delegate void OnDisableCallback(EnemyProjectileBasic instance);
    public event OnDisableCallback OnDisableAction;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
    }


    private void ActivateVisuals()
    {
        trailRenderer.enabled = true;
        spriteRenderer.enabled = true;

    }

    public void DisableVisuals()
    {
        trailRenderer.enabled = false;
        spriteRenderer.enabled = false;

    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void Shoot(Vector3 position, Vector3 direction, float newSpeed, float newLifeTime, float newDamage)
    {
        transform.forward = direction;
        transform.position = position;

        

        trailRenderer.Clear();
        speed = newSpeed;
        lifeTime = newLifeTime;
        damage = newDamage;

        Invoke("ActivateVisuals", 0.05f);
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
        transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            OnDisableAction?.Invoke(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.Instance.TakeDamage(damage);
            OnDisableAction?.Invoke(this);
        }
        else
        {
            OnDisableAction?.Invoke(this);
        }
    }

    public override bool Parry()
    {
        Vector3 direction = PlayerReferences.Instance.LookPoint.transform.forward;
        float parriedSpeed = ConstantValues.PARRIED_PROJECTILE_SPEED;
        ParriedProjectilesObjectPool.Instance.ShootBullet(transform.position, direction, parriedSpeed, lifeTime, damage);
        OnDisableAction?.Invoke(this);
        base.Parry();
        return true;
    }

}
