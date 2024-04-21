using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBase : MonoBehaviour, Ihp
{

    [SerializeField] float maxHp;
    [SerializeField] GameObject enemyMainBody;
    private float currentHp;

    //Event który triggeruje siê gdy przeciwnik umiera
    public delegate void EnemyDeathAction();
    public static event EnemyDeathAction OnEnemyDeath;


    private void Start()
    {
        currentHp = maxHp;
    }
    public virtual void Die()
    {
        if(enemyMainBody != null)
        {
            enemyMainBody.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
    }

    public virtual void Heal(float healAmount)
    {
        if (currentHp + healAmount <= maxHp)
        {
            currentHp += healAmount;
        }
        else
        {
            currentHp = maxHp;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log("enemy took " + damage + " damage");
        if (currentHp < 0)
        {
            Die();
        }
    }

}
