using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/OneTimeUse/AdrenalineInjectorBH")]
public class AdrenalineInjectorBH : ItemBehaviour
{
    [SerializeField] private float timeSlowPower = 0.25f;
    [SerializeField] private float timeSlowDuration = 1f;

    public override void OnPlayerTakeDamage()
    {
        GameStateManager.Instance.StartSlowTime(timeSlowPower, timeSlowDuration);
    }
}
