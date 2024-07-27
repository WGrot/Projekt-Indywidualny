using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviourManager
{
    private List<Action> onPickupBH;
    private List<Action> onDropBH;
    private List<Action> onPlayerTakeDamageBH;
    public ItemBehaviourManager()
    {
        onDropBH = new List<Action>();
        onPickupBH = new List<Action>();
        onPlayerTakeDamageBH= new List<Action>();
        SubToAllEvents();
    }

    public void AddFuncToOnPlayerTakeDamageBH(Action func)
    {
        onPlayerTakeDamageBH.Add(func);

    }

    private void SubToAllEvents()
    {
        PlayerStatus.OnPlayerTakeDamageCallback += OnPlayerTakeDamage;
    }

    private void UnSubToAllEvents()
    {

    }

    private void OnPlayerTakeDamage()
    {
        DoActions(onPlayerTakeDamageBH);
    }


    private void DoActions(List<Action> functions)
    {
        foreach(Action func in functions)
        {
            func();
        }
    }
}
