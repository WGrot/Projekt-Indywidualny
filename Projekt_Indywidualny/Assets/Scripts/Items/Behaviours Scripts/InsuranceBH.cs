using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/InsuranceBH")]
public class InsuranceBH : ItemBehaviour
{
    public override void OnCoinAmountChange(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        if(amount > 0)
        {
            PlayerStatus.Instance.Heal(amount);
        }
    }
}
