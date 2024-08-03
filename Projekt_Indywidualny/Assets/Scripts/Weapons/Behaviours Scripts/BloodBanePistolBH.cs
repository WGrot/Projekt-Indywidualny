using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/BloodBanePistolBH")]
public class BloodBanePistolBH : WeaponBehaviour
{
    public float penaltyForShoot = 2;
    public float healForKillAmount = 5;
    public override void OnShoot()
    {
        PlayerStatus.Instance.TakeDamage(2);
    }

    public override void OnEnemyDeath()
    {
        PlayerStatus.Instance.Heal(5);
    }
}
