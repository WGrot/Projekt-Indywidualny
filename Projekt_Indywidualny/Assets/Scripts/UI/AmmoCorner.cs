using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCorner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoInClipText;
    [SerializeField] private TextMeshProUGUI totalAmmoData;

    private void OnEnable()
    {
        WeaponHolder.OnWeaponShootCallback += ShowAmmoData;
        WeaponHolder.OnWeaponChangedCallback += ShowAmmoData;
        WeaponHolder.OnWeaponReloadCallback += ShowAmmoData;
    }

    private void OnDisable()
    {
        WeaponHolder.OnWeaponShootCallback -= ShowAmmoData;
        WeaponHolder.OnWeaponChangedCallback -= ShowAmmoData;
        WeaponHolder.OnWeaponReloadCallback -= ShowAmmoData;
    }

    private void ShowAmmoData(int activeWeaponId)
    {
        Debug.Log(activeWeaponId);
        string ammoLeft = Inventory.Instance.GetAmmoAtIndex(activeWeaponId).ammoLeft.ToString();
        string maxAmmo = Inventory.Instance.GetWeaponAtIndex(activeWeaponId).MaxAmmo.ToString();
        string ammoinClipString = Inventory.Instance.GetAmmoAtIndex(activeWeaponId).ammoInClip.ToString(); 
        string ammoCombined = ammoLeft + "/" + maxAmmo;
        totalAmmoData.SetText(ammoCombined);
        ammoInClipText.SetText(ammoinClipString);
    }
}
