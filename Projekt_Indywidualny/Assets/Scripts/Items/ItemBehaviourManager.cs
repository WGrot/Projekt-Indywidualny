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
    private List<Action> onLevelGeneratedBH;
    private List<Action> onPlayerDeathBH;
    private List<Action> SuccessfulParryBH;
    private List<Action<int>> onCoinAmountChangeBH;
    public ItemBehaviourManager()
    {
        onDropBH = new List<Action>();
        onPickupBH = new List<Action>();

        onPlayerTakeDamageBH = new List<Action>();
        onEnemyDeathBH = new List<Action>();
        onLevelGeneratedBH = new List<Action>();
        onPlayerDeathBH = new List<Action>();
        SuccessfulParryBH = new List<Action>();
        onCoinAmountChangeBH = new List<Action<int>>();

        SubToAllEvents();
    }

    private void SubToAllEvents()
    {
        PlayerStatus.OnPlayerTakeDamageCallback += OnPlayerTakeDamage;
        EnemyHpBase.OnEnemyDeath += OnEnemyDeath;
        PlayerStatus.OnCoinsAmountChangeCallback += OnCoinAmountChange;
        PlayerStatus.OnPlayerDieCallback += OnPlayerDeath;
        LevelGenerationV2.OnLevelGenerated += OnLevelGenerated;
        FlipHitbox.OnSuccessfulParryActionCallback += OnSuccessfulParry;
    }

    public void UnSubToAllEvents()
    {
        PlayerStatus.OnPlayerTakeDamageCallback -= OnPlayerTakeDamage;
        EnemyHpBase.OnEnemyDeath -= OnEnemyDeath;
        PlayerStatus.OnCoinsAmountChangeCallback -= OnCoinAmountChange;
        PlayerStatus.OnPlayerDieCallback -= OnPlayerDeath;
        LevelGenerationV2.OnLevelGenerated -= OnLevelGenerated;
        FlipHitbox.OnSuccessfulParryActionCallback -= OnSuccessfulParry;
    }

    public void ClearAllBehaviours()
    {
        onPlayerTakeDamageBH.Clear();
        onLevelGeneratedBH.Clear();
        onCoinAmountChangeBH.Clear();
        onEnemyDeathBH.Clear();
        onPlayerDeathBH.Clear();
    }

    public static void ResetAllBehaviours()
    {
        //Reseting item behaviours
        UnityEngine.Object[] behaviourObjects = Resources.LoadAll("ItemsResources/Behaviours");
        ItemBehaviour[] allItemBehaviours = new ItemBehaviour[behaviourObjects.Length];
        behaviourObjects.CopyTo(allItemBehaviours, 0);

        foreach(ItemBehaviour behaviour in allItemBehaviours)
        {
            behaviour.ResetBehaviour();
        }

        //Reseting item weapon behaviours
        UnityEngine.Object[] weaponBehaviourObjects = Resources.LoadAll("WeaponsResources/Behaviours");
        ItemBehaviour[] allWeaponBehaviours = new ItemBehaviour[weaponBehaviourObjects.Length];
        weaponBehaviourObjects.CopyTo(allWeaponBehaviours, 0);

        foreach (ItemBehaviour behaviour in allWeaponBehaviours)
        {
            behaviour.ResetBehaviour();
        }
    }


    private void DoActions(List<Action> functions)
    {
        foreach (Action func in functions)
        {
            func();
        }
    }

    private void DoCoinActions(List<Action<int>> functions, int amount)
    {
        foreach (Action<int> func in functions)
        {
            func(amount);
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

    #region OnLevelGenerated list methods
    private void OnLevelGenerated()
    {
        DoActions(onLevelGeneratedBH);
    }

    public void AddFuncToOnLevelGenrated(Action func)
    {
        onLevelGeneratedBH.Add(func);

    }
    public void RemoveFuncFromOnLevelGenerated(Action func)
    {
        onLevelGeneratedBH.Remove(func);

    }
    #endregion

    #region OnPlayerDeath list methods
    private void OnPlayerDeath()
    {
        DoActions(onPlayerDeathBH);
    }

    public void AddFuncToOnPlayerDeath(Action func)
    {
        onPlayerDeathBH.Add(func);

    }
    public void RemoveFuncFromOnPlayerDeath(Action func)
    {
        onPlayerDeathBH.Remove(func);

    }
    #endregion

    #region OnSuccessfulParry list methods
    private void OnSuccessfulParry()
    {
        DoActions(onPlayerDeathBH);
    }

    public void AddFuncToOnSuccessfulParry(Action func)
    {
        onPlayerDeathBH.Add(func);

    }
    public void RemoveFuncFromOnSuccessfulParry(Action func)
    {
        onPlayerDeathBH.Remove(func);

    }
    #endregion

    #region OnCoinAmountChange list methods
    private void OnCoinAmountChange(int amount)
    {
        DoCoinActions(onCoinAmountChangeBH, amount);
    }

    public void AddFuncToOnCoinAmountChange(Action<int> func)
    {
        onCoinAmountChangeBH.Add(func);

    }
    public void RemoveFuncFromOnCoinAmountChange(Action<int> func)
    {
        onCoinAmountChangeBH.Remove(func);

    }
    #endregion

}
