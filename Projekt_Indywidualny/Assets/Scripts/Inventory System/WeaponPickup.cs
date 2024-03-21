using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : ItemPickup
{
    public bool isWeaponRandom = true;

    public override void Start()
    {
        ChooseRandomWeapon();
        WeaponSO weapon = (WeaponSO)item;
        weapon.SetPrefix(ChooseRandomPrefix());
        base.item= weapon;
        itemSprite.sprite = item.icon;
    }
    public override void InteractWithPlayer()
    {
        if (item is WeaponSO)
        {
            Inventory.Instance.AddWeapon((WeaponSO)item);
        }
        else
        {
            Debug.LogError("You configures something wrong, you used weapon pickap with item that is not a weapon");
        }
        Destroy(gameObject);
    }

    private void ChooseRandomWeapon()
    {
        int result = Random.Range(1,100);
        if(result < 50) //50% chance for common weapon
        {
            Object[] weaponObjects = Resources.LoadAll("WeaponsResources/Weapons/Common");
            WeaponSO[] commonWeapons = new WeaponSO[weaponObjects.Length];
            weaponObjects.CopyTo(commonWeapons, 0);

            base.item = commonWeapons[Random.Range(0, weaponObjects.Length)];
            
        }else if (result >= 50 && result < 80) // 30% chance for uncommon weapon
        {
            Object[] weaponObjects = Resources.LoadAll("WeaponsResources/Weapons/Uncommon");
            WeaponSO[] uncommonWeapons = new WeaponSO[weaponObjects.Length];
            weaponObjects.CopyTo(uncommonWeapons, 0);

            base.item = uncommonWeapons[Random.Range(0, weaponObjects.Length)];
        }
        else if (result >= 80 && result < 95) // 15% chance for rare weapon
        {
            Object[] weaponObjects = Resources.LoadAll("WeaponsResources/Weapons/Rare");
            WeaponSO[] rareWeapons = new WeaponSO[weaponObjects.Length];
            weaponObjects.CopyTo(rareWeapons, 0);

            base.item = rareWeapons[Random.Range(0, weaponObjects.Length)];
        }
        else // Last 5% chance for Mythic weapon
        {
            Object[] weaponObjects = Resources.LoadAll("WeaponsResources/Weapons/Mythic");
            WeaponSO[] mythicWeapons = new WeaponSO[weaponObjects.Length];
            weaponObjects.CopyTo(mythicWeapons, 0);

            base.item = mythicWeapons[Random.Range(0, weaponObjects.Length)];
        }
    }

    private WeaponPrefix ChooseRandomPrefix()
    {
        int result = Random.Range(1, 100);
        WeaponPrefix chosenPrefix;
        if (result < 50) 
        {
            Object[] prefixesObjects = Resources.LoadAll("WeaponsResources/WeaponPrefixes/Common");
            WeaponPrefix[] commonPrefixes = new WeaponPrefix[prefixesObjects.Length];
            prefixesObjects.CopyTo(commonPrefixes, 0);

            chosenPrefix = commonPrefixes[Random.Range(0, prefixesObjects.Length)];

        }
        else if (result >= 50 && result < 80)
        {
            Object[] prefixesObjects = Resources.LoadAll("WeaponsResources/WeaponPrefixes/Uncommon");
            WeaponPrefix[] uncommonPrefixes = new WeaponPrefix[prefixesObjects.Length];
            prefixesObjects.CopyTo(uncommonPrefixes, 0);

            chosenPrefix = uncommonPrefixes[Random.Range(0, prefixesObjects.Length)];
        }
        else if (result >= 80 && result < 95)
        {
            Object[] prefixesObjects = Resources.LoadAll("WeaponsResources/WeaponPrefixes/Rare");
            WeaponPrefix[] rarePrefixes = new WeaponPrefix[prefixesObjects.Length];
            prefixesObjects.CopyTo(rarePrefixes, 0);

            chosenPrefix = rarePrefixes[Random.Range(0, prefixesObjects.Length)];
        }
        else
        {
            Object[] prefixesObjects = Resources.LoadAll("WeaponsResources/WeaponPrefixes/Mythic");
            WeaponPrefix[] mythicPrefixes = new WeaponPrefix[prefixesObjects.Length];
            prefixesObjects.CopyTo(mythicPrefixes, 0);

            chosenPrefix = mythicPrefixes[Random.Range(0, prefixesObjects.Length)];
        }
        return chosenPrefix;
    }
}
