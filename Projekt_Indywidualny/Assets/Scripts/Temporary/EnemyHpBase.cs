using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBase : MonoBehaviour, Ihp
{

    [SerializeField] protected float maxHp;
    [SerializeField] GameObject enemyMainBody;
    [SerializeField] private EnemyDrop enemyDrop;
    protected float currentHp;


    //Event kt�ry triggeruje si� gdy przeciwnik umiera
    public delegate void EnemyDeathAction();
    public static event EnemyDeathAction OnEnemyDeath;


    public virtual void Start()
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

        if (enemyDrop!= null)
        {
            enemyDrop.Drop();
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
