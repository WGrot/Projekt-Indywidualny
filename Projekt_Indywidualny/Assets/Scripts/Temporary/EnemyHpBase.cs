using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBase : MonoBehaviour, Ihp
{

    [SerializeField] float maxHp;
    private float currentHp;

    //Event który triggeruje siê gdy przeciwnik umiera
    public delegate void EnemyDeathAction();
    public static event EnemyDeathAction OnEnemyDeath;


    private void Start()
    {
        currentHp = maxHp;
    }
    public void Die()
    {
        gameObject.SetActive(false);
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
    }

    public void Heal(float healAmount)
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

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player killed an enemy");
        if (other.CompareTag("Player"))
        {
            Die();
        }
    }
}
