using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapon Behaviour")]
public class WeaponBehaviour : ItemBehaviour
{
    public virtual void OnShoot()
    {
        Debug.Log("OnShoot behaviour works");
        PlayerStatus.Instance.ReduceCurrentPlayerHP(1);
    }
    public virtual void OnReload()
    {
        Debug.Log("OnReload behaviour works");
    }

    public virtual void OnEquip()
    {
        Debug.Log("OnEquip behaviour works");
    }
}
