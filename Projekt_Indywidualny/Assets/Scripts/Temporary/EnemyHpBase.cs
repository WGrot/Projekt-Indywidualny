using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBase : MonoBehaviour, Ihp
{

    [SerializeField] protected float maxHp;
    [SerializeField] GameObject enemyMainBody;
    [SerializeField] private EnemyDrop enemyDrop;
    protected AudioSource audioSource;
    protected float currentHp;

    [SerializeField] private GameObject deathProp;
    [SerializeField] AudioClip takeDamageSound;

    //Event który triggeruje siê gdy przeciwnik umiera
    public delegate void EnemyDeathAction();
    public static event EnemyDeathAction OnEnemyDeath;


    public virtual void Start()
    {
        currentHp = maxHp;
        audioSource = GetComponent<AudioSource>();
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

        if (deathProp != null)
        {
            Instantiate(deathProp, transform.position, Quaternion.identity);
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
        if (audioSource != null && takeDamageSound != null)
        {
            audioSource.PlayOneShot(takeDamageSound);
            //audioSource.clip = takeDamageSound;
           // audioSource.Play();
        }

        if (currentHp < 0)
        {
            Die();
        }
    }

}
