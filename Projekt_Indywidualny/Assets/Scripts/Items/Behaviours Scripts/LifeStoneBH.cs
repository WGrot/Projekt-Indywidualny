using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/LifeStoneBH")]
public class LifeStoneBH : ItemBehaviour
{
    [SerializeField] private float lifeGain;
    public override void OnPickup()
    {
        PlayerStatus.Instance.IncreaseCurrentPlayerHP(lifeGain);
    }

    public override void OnDrop()
    {
        PlayerStatus.Instance.ReduceCurrentPlayerHP(lifeGain);
    }
}
