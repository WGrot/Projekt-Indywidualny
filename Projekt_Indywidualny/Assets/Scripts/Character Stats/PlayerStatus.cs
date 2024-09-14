using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerStatus;

public class PlayerStatus : MonoBehaviour
{
    public List<CharacterStat> stats = new List<CharacterStat>();
    PL_HealthStat hpStat;
    private float currentHp;
    private int coins = 0;
    private bool canTakeDamage = true;
    private bool isPlayerDead = false;
    public static PlayerStatus Instance { get; private set; }
    public bool IsPlayerDead { get => isPlayerDead; private set => isPlayerDead = value; }
    public int Coins { get => coins; private set => coins = value; }

    private GameObject playerBody;

    #region events

    public delegate void OnPlayersHealthChanged();
    public static event OnPlayersHealthChanged OnPlayersHealthChangedCallback;

    public delegate void OnPlayerTakeDamage();
    public static event OnPlayerTakeDamage OnPlayerTakeDamageCallback;

    public delegate void OnPlayerHeal();
    public static event OnPlayerHeal OnPlayerHealCallback;

    public delegate void OnPlayerDie();
    public static event OnPlayerDie OnPlayerDieCallback;

    public delegate void OnCoinsAmountChange(int amount);
    public static event OnCoinsAmountChange OnCoinsAmountChangeCallback;

    #endregion

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

    public void AssignPlayerBody()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player");
    }
    public void AssignPlayerBody(GameObject body)
    {
        playerBody = body;
    }

    public GameObject GetPlayerBody()
    {
        return playerBody;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerBody.transform.position;   
    }

    public Quaternion GetPlayerRotation()
    {
        return playerBody.transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MusicManager.Instance.PlayFightMusic();
            
            
            //Debug.Log("painkiller");
            //GameStateManager.Instance.StartSlowTime(0.25F, 4);
            /*
            TakeDamage(10);
            for (int i = 0; i< stats.Count; i++) {
                Debug.Log(stats[i].value);

            }
            Debug.Log("PlayerHP = " + currentHp);
            */
        }

    }

    public void Die()
    {
        Debug.Log("PlayerIsDead");
        IsPlayerDead = true;
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
        }
        else
        {
            currentHp = stats[0].value;
        }

        if (OnPlayerHealCallback != null)
        {
            OnPlayerHealCallback();

        }

        if(OnPlayersHealthChangedCallback != null)
        {
            OnPlayersHealthChangedCallback();
        }

    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        currentHp -= damage * (1 - (0.05f * stats[1].value / 10));
        
        if (OnPlayerTakeDamageCallback != null)
        {
            OnPlayerTakeDamageCallback();
        }

        if (OnPlayersHealthChangedCallback != null)
        {
            OnPlayersHealthChangedCallback();
        }

        if (currentHp <= 0 && !IsPlayerDead)
        {
            Die();
        }
    }

    public void ReduceCurrentPlayerHP(float amount)
    {
        Debug.Log(currentHp);
        currentHp -= amount;
        Debug.Log(currentHp);
        if (OnPlayersHealthChangedCallback != null)
        {
            OnPlayersHealthChangedCallback();
        }

        if (currentHp <= 0 && !IsPlayerDead)
        {
            Die();
        }
    }

    public void IncreaseCurrentPlayerHP(float amount)
    {
        if (currentHp + amount <= stats[0].value)
        {
            currentHp += amount;
        }
        else
        {
            currentHp = stats[0].value;
        }

        if (OnPlayersHealthChangedCallback != null)
        {
            OnPlayersHealthChangedCallback();
        }
    }
    
    public float GetCharacterStatValueOfType(StatType type)
    {
        foreach(CharacterStat stat in stats)
        {
            if(stat.statType == type)
            {
                return stat.value;
            }
        }
        Debug.Log("Coœ posz³o nie tak, chcesz dostaæ wartoœæ statystyki która nie istnieje");
        return 1;
    }


    public void AddCoins(int amount)
    {
        coins += amount;
        if (OnCoinsAmountChangeCallback != null)
        {
            OnCoinsAmountChangeCallback(amount);
        }
    }
    
    public bool RemoveCoins(int amount)
    {

        if (coins >= amount)
        {
            coins -= amount;
            if (OnCoinsAmountChangeCallback != null)
            {
                OnCoinsAmountChangeCallback(-amount);
            }
            return true;
        }
        return false;
    }

}
