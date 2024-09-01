using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Behaviour")]
public class ItemBehaviour : ScriptableObject
{
    public virtual void OnPickup()
    {
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

}
