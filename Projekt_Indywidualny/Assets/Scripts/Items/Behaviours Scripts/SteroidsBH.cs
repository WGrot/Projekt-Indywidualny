using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/SteroidsBH")]
public class SteroidsBH : ItemBehaviour
{
    [SerializeField] private float LifeDecrease = 50f;

    public override void OnPickup()
    {
        PlayerStatus.Instance.ReduceCurrentPlayerHP(LifeDecrease);
    }

}
