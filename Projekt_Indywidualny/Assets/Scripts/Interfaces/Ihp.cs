using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Ihp
{
    public void TakeDamage(float damage);
    public void Heal(float healAmount);
    public void Die();

}
