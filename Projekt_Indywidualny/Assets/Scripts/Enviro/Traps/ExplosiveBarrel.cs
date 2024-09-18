using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Explosive, Ihp
{
    [SerializeField] private float startBarrelHp;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float currentBarrelHp;
    public void Die()
    {
        StartCoroutine(Explode());
    }

    public void Heal(float healAmount){ }
    

    public void TakeDamage(float damage)
    {
        currentBarrelHp -= damage;
        if(currentBarrelHp < 0)
        {
            Die();
        }
    }

    public override void Start()
    {
        base.Start();
        currentBarrelHp = startBarrelHp;
    }

    protected override void DisableVisuals()
    {
        spriteRenderer.enabled = false;
    }
}
