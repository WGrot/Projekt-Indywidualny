using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    protected Rigidbody rb;


    public void SetDamage(float realDamage)
    {
        this.damage = realDamage;
    }


}
