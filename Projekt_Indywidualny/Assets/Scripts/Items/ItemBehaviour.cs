using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Behaviour")]
public class ItemBehaviour : ScriptableObject
{
    [SerializeField] protected bool wasPickedUpBefore = false;

    public virtual void ResetBehaviour()
    {
        wasPickedUpBefore = false;
    }
    public virtual void OnPickup()
    {
        wasPickedUpBefore = true;

    }
    public virtual void OnDrop()
    {

    }
    public virtual void OnPlayerTakeDamage()
    {

    
    }
    public virtual void OnEnemyDeath(){


    }

    public virtual void OnCoinAmountChange(int amount)
    {


    }

    public virtual void OnLevelGenerated()
    {

    }

    public virtual void OnPlayerDeath()
    {

    }

    public virtual void OnSuccessfulParry()
    {

    }
}
