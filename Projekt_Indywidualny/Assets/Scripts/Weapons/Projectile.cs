using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, I_Parryable
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    protected Rigidbody rb;

    public virtual bool Parry()
    {
        GameStateManager.Instance.StartSlowTime(0.1f, 0.2f);
        return true;
    }

    public void SetDamage(float realDamage)
    {
        this.damage = realDamage;
    }


}
