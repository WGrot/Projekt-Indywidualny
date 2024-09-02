using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/LifeStoneBH")]
public class LifeStoneBH : ItemBehaviour
{
    [SerializeField] private float lifeGain;
    public override void OnPickup()
    {
        if (!wasPickedUpBefore)
        {
            PlayerStatus.Instance.IncreaseCurrentPlayerHP(lifeGain);
        }
        base.OnPickup();
    }


}
