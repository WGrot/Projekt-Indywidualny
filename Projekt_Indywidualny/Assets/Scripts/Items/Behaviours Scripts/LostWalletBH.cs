using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/LostWalletBH")]
public class LostWalletBH : ItemBehaviour
{
    [SerializeField] private int coinsInWallet;
    public override void OnPickup()
    {
        if (!wasPickedUpBefore)
        {
            PlayerStatus.Instance.AddCoins(coinsInWallet);
        }
        base.OnPickup();
    }
}
