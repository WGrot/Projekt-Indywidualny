using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public List<CharacterStat> stats = new List<CharacterStat>();
    private float currentHp;
    private bool canTakeDamage = true;

    public static PlayerStatus Instance { get; private set; }


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
    }

    void Start()
    {
        currentHp = stats[0].value;
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i< stats.Count; i++) {
                Debug.Log(stats[i].value);

            }
            Debug.Log("PlayerHP = " + currentHp);
        }
        if (currentHp <= 0)
        {
            Debug.Log("POlayerDead");
        }
    }

    public void Die()
    {
        Debug.Log("PlayerIsDead");
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

    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }
    

}
