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
        Debug.Log("Funkcja OnPickup dziala");
    }
    public virtual void OnDrop()
    {
    Debug.Log("Funkcja OnDrop dziala");
    }
    public virtual void OnPlayerTakeDamage()
    {
        Debug.Log("dziala?");
    
    }
    public virtual void OnEnemyDeath(){

        Debug.Log("dobrze vi tak skowyrsynie");
    }

    public virtual void OnCoinAmountChange(int amount)
    {

        Debug.Log("dmy moni");
    }

    public virtual void OnLevelGenerated()
    {
        Debug.Log("behaviour on level generated");
    }

    public virtual void OnPlayerDeath()
    {
        Debug.Log("behaviour on PlayerDeath");
    }

    public virtual void OnSuccessfulParry()
    {
        Debug.Log("behaviour on SuccessfulParry");
    }
}
