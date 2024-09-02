using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/ShopKeepersHatBH")]
public class ShopkeepersHatBH : ItemBehaviour
{
    [SerializeField] private PassiveItem shopkeepersHatMainSO;
    [SerializeField] private bool isWorking = true;
    public override void OnCoinAmountChange(int amount)
    {
        if (amount < 0)
        {
            isWorking= false;
        }
    }

    public override void OnLevelGenerated()
    {
        if (!isWorking)
        {
            return;
        }

        PlayerStatus.Instance.AddCoins(PlayerStatus.Instance.Coins);
    }

    public override void ResetBehaviour()
    {
        base.ResetBehaviour();
        isWorking = true;
    }

}
