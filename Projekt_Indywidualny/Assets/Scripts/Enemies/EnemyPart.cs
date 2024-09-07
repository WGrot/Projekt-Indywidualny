using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPart : MonoBehaviour, Ihp
{
    [SerializeField] private GameObject enemyMainPart;
    [SerializeField] private float damageModifier;
    public void Die(){}

    public void Heal(float healAmount){}

    public void TakeDamage(float damage)
    {
        enemyMainPart.GetComponent<Ihp>().TakeDamage(damage * damageModifier);
    }

}
