using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviourManager
{
    private List<Action> onPickupBH;
    private List<Action> onDropBH;

    private List<Action> onPlayerTakeDamageBH;
    private List<Action> onEnemyDeathBH;
    private List<Action> onCoinAmountChangeBH;
    public ItemBehaviourManager()
    {
        onDropBH = new List<Action>();
        onPickupBH = new List<Action>();

        onPlayerTakeDamageBH= new List<Action>();
        onEnemyDeathBH= new List<Action>();
        onCoinAmountChangeBH= new List<Action>();

        SubToAllEvents();
    }

    private void SubToAllEvents()
    {
        PlayerStatus.OnPlayerTakeDamageCallback += OnPlayerTakeDamage;
        EnemyHpBase.OnEnemyDeath += OnEnemyDeath;
        PlayerStatus.OnCoinsAmountChangeCallback += OnCoinAmountChange;
    }

    private void UnSubToAllEvents()
    {
        PlayerStatus.OnPlayerTakeDamageCallback -= OnPlayerTakeDamage;
        EnemyHpBase.OnEnemyDeath -= OnEnemyDeath;
        PlayerStatus.OnCoinsAmountChangeCallback -= OnCoinAmountChange;
    }
    private void DoActions(List<Action> functions)
    {
        foreach (Action func in functions)
        {
            func();
        }
    }

    #region OnPlayerTakeDamage list methods
    private void OnPlayerTakeDamage()
    {
        DoActions(onPlayerTakeDamageBH);
    }

    public void AddFuncToOnPlayerTakeDamageBH(Action func)
    {
        onPlayerTakeDamageBH.Add(func);

    }
    public void RemoveFuncFromOnPlayerTakeDamageBH(Action func)
    {
        onPlayerTakeDamageBH.Remove(func);

    }
    #endregion

    #region OnEnemyDeath list methods
    private void OnEnemyDeath()
    {
        DoActions(onEnemyDeathBH);
    }

    public void AddFuncToOnEnemyDeath(Action func)
    {
        onEnemyDeathBH.Add(func);

    }
    public void RemoveFuncFromOnEnemyDeath(Action func)
    {
        onEnemyDeathBH.Remove(func);

    }
    #endregion

    #region OnCoinAmountChange list methods
    private void OnCoinAmountChange(int amount)
    {
        DoActions(onCoinAmountChangeBH);
    }

    public void AddFuncToOnCoinAmountChange(Action func)
    {
        onCoinAmountChangeBH.Add(func);

    }
    public void RemoveFuncFromOnCoinAmountChange(Action func)
    {
        onCoinAmountChangeBH.Remove(func);

    }
    #endregion

}
