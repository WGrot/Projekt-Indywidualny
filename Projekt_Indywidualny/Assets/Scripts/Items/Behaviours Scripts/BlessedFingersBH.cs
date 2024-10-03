using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/BlessedFingersBH")]
public class BlessedFingersBH : ItemBehaviour
{
    [SerializeField] private int HealPercent;
    public override void OnSuccessfulParry()
    {
        PlayerStatus.Instance.HealByPercent(HealPercent);
        base.OnSuccessfulParry();
    }

}
