using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, Ihp
{
    public void Die()
    {
        PlayerStatus.Instance.Die();
    }

    public void Heal(float healAmount)
    {
        PlayerStatus.Instance.Heal(healAmount);
    }

    public void TakeDamage(float damage)
    {
        PlayerStatus.Instance.TakeDamage(damage);
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
