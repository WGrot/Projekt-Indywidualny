using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParriedProjectileBasic : Projectile
{
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    public delegate void OnDisableCallback(ParriedProjectileBasic instance);
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
        ActivateVisuals();
       // Invoke("ActivateVisuals", 0.05f);
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
        transform.position = transform.position + (speed * Time.deltaTime * transform.forward);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            OnDisableAction?.Invoke(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with something");
        if (other.transform.TryGetComponent<Ihp>(out var enemyHp) && other.CompareTag("Enemy"))
        {
            Debug.Log("TouchedEnemy");
            enemyHp.TakeDamage(damage * PlayerStatus.Instance.GetCharacterStatValueOfType(StatType.Damage));
            OnDisableAction?.Invoke(this);
        }
        else
        {
            OnDisableAction?.Invoke(this);
        }
    }

    public override void Parry(){}
}
