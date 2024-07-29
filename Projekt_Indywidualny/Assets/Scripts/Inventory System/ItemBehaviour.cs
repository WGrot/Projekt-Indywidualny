using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Behaviour")]
public class ItemBehaviour : ScriptableObject
{
    public Action action;
    public int inter;
    public virtual void OnPickup()
    {
        Debug.Log("Funkcja OnPickup dziala");
        PlayerStatus.Instance.ReduceCurrentPlayerHP(10);
    }
    public virtual void OnDrop()
    {
    Debug.Log("Funkcja OnDrop dziala");
    PlayerStatus.Instance.IncreaseCurrentPlayerHP(10);
    }
    public virtual void OnPlayerTakeDamage()
    {
        Debug.Log("dziala?");
    
    }
    public virtual void OnEnemyDeath(){

        Debug.Log("dobrze vi tak skowyrsynie");
    }

}
