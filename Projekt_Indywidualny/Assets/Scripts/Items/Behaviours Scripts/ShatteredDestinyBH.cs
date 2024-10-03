using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/ShatteredDestinyBH")]
public class ShatteredDestinyBH : ItemBehaviour
{
    public override void OnPickup()
    {
        if (!wasPickedUpBefore)
        {
            PlayerStatus.Instance.IncrementRevivals();
        }
        base.OnPickup();
    }
}
