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
        WeaponHolder.OnWeaponChangedCallback += ShowAmmoData;
        AmmoData.OnAmmoDataChangedCallback += ShowAmmoData;
    }

    private void OnDisable()
    {
        WeaponHolder.OnWeaponChangedCallback -= ShowAmmoData;
        AmmoData.OnAmmoDataChangedCallback -= ShowAmmoData;
    }


    private void ShowAmmoData(int activeWeaponId)
    {
        string ammoLeft = Inventory.Instance.GetAmmoAtIndex(activeWeaponId).ammoLeft.ToString();
        string maxAmmo = Inventory.Instance.GetWeaponAtIndex(activeWeaponId).MaxAmmo.ToString();
        string ammoinClipString = Inventory.Instance.GetAmmoAtIndex(activeWeaponId).ammoInClip.ToString();
        string ammoCombined = ammoLeft + "/" + maxAmmo;
        totalAmmoData.SetText(ammoCombined);
        ammoInClipText.SetText(ammoinClipString);
    }
    private void ShowAmmoData()
    {
        int activeWeaponId = Inventory.Instance.GetActiveWeaponID();
        string ammoLeft = Inventory.Instance.GetAmmoAtIndex(activeWeaponId).ammoLeft.ToString();
        string maxAmmo = Inventory.Instance.GetWeaponAtIndex(activeWeaponId).MaxAmmo.ToString();
        string ammoinClipString = Inventory.Instance.GetAmmoAtIndex(activeWeaponId).ammoInClip.ToString(); 
        string ammoCombined = ammoLeft + "/" + maxAmmo;
        totalAmmoData.SetText(ammoCombined);
        ammoInClipText.SetText(ammoinClipString);
    }
}
