using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Behaviour")]
public class ItemBehaviour : ScriptableObject
{
    public Action action;
    public int inter;
    public virtual void OnPickup(){}
    public virtual void OnDrop(){}
    public virtual void OnPlayerTakeDamage()
    {
        Debug.Log("dziala?");
    
    }
    public virtual void OnEnemyDeath() { }

}
