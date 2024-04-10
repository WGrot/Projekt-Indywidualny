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
            Debug.Log("POlayerDead");
        }
    }

    public void Die()
    {
        Debug.Log("PlayerIsDead");
        if (OnPlayerDieCallback != null)
        {
            OnPlayerDieCallback();
        }
    }

    public void Heal(float healAmount)
    {
        if (currentHp + healAmount <= stats[0].value)
        {
            currentHp += healAmount;
            if (OnPlayerHealCallback != null)
            {
                OnPlayerHealCallback();
            }
        }
        else
        {
            currentHp = stats[0].value;
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
        if (currentHp <= 0)
        {
            Die();
        }
    }
    

}
