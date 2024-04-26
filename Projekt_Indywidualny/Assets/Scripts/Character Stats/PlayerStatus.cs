using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public List<CharacterStat> stats = new List<CharacterStat>();
    PL_HealthStat hpStat;
    private float currentHp;
    private bool canTakeDamage = true;
    private bool isPlayerDead = false;

    public static PlayerStatus Instance { get; private set; }

    public delegate void OnPlayerTakeDamage();
    public static event OnPlayerTakeDamage OnPlayerTakeDamageCallback;
    public delegate void OnPlayerHeal();
    public static event OnPlayerHeal OnPlayerHealCallback;
    public delegate void OnPlayerDie();
    public static event OnPlayerDie OnPlayerDieCallback;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one PlayerStatus instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        hpStat = new PL_HealthStat(stats[0].value);
        stats[0] = hpStat;
        currentHp = stats[0].value;
    }


    public float GetCurrentHp()
    {
        return currentHp;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
            for (int i = 0; i< stats.Count; i++) {
                Debug.Log(stats[i].value);

            }
            Debug.Log("PlayerHP = " + currentHp);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Heal(10);

        }
        if (currentHp <= 0)
        {

        }
    }

    public void Die()
    {
        Debug.Log("PlayerIsDead");
        isPlayerDead = true;
        if (OnPlayerDieCallback != null)
        {
            OnPlayerDieCallback();
        }
    }

    public void Heal(float healAmount)
    {
        Debug.Log("healoed");
        if (currentHp + healAmount <= stats[0].value)
        {
            currentHp += healAmount;
        }
        else
        {
            Debug.Log("chuj " + stats[0].value);
            currentHp = stats[0].value;
        }
        if (OnPlayerHealCallback != null)
        {
            OnPlayerHealCallback();
        }

    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        currentHp -= damage;
        if (OnPlayerTakeDamageCallback != null)
        {
            OnPlayerTakeDamageCallback();
        }
        Debug.Log("PlayerTook " + damage + "damage");
        if (currentHp <= 0 && !isPlayerDead)
        {
            Die();
        }
    }
    

}
