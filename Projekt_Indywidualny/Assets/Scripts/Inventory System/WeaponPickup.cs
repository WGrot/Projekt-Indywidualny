using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : ItemPickup
{
    [SerializeField] private bool isWeaponRandom = true;
    [SerializeField] private bool isPrefixRandom = true;
    [SerializeField] private WeaponSO weapon;
    [SerializeField] private WeaponPrefix prefix;
    [SerializeField] private AmmoData ammoData;
    public override void Start()
    {

        if (isWeaponRandom)
        {
            ChooseRandomWeapon();
            weapon = (WeaponSO)item;
            ammoData = new AmmoData(((WeaponSO)item).ClipSize, ((WeaponSO)item).MaxAmmo); ;
        }

        if (isPrefixRandom)
        {
            prefix = ChooseRandomPrefix();
        }
        if (weapon != null)
        {
            base.item = weapon;
            itemSprite.sprite = item.icon;
        }

    }

    public void SetWeaponAndPrefixAndAmmo(WeaponSO weapon, WeaponPrefix prefix, AmmoData ammo)
    {

        this.prefix= prefix;
        base.item = weapon; 
        this.ammoData = ammo;
        itemSprite.sprite = item.icon;
    }
    public override void InteractWithPlayer()
    {


        if (item is WeaponSO)
        {
            if (Inventory.Instance.IsWeaponAlreadyInList((WeaponSO)item))
            {
                Debug.Log(Inventory.Instance.GetWeaponIndex((WeaponSO)item));
                GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponHolder>().DropWeaponAtIndex(Inventory.Instance.GetWeaponIndex((WeaponSO)item));
            }
            Inventory.Instance.AddPrefix(prefix);   //Najpierw trzeba dodaæ prefix dopiero póŸniej broñ bo inaczej sypie nullPointerException
            Inventory.Instance.AddWeapon((WeaponSO)item);
            Inventory.Instance.AddAmmo(ammoData);
        }
        else
        {
            Debug.LogError("You configured something wrong, you used weapon pickup with item that is not a weapon");
        }
        Destroy(gameObject);
    }

    private void ChooseRandomWeapon()
    {
        int result = Random.Range(1, 100);
        if (result < 50) //50% chance for common weapon
        {
            Object[] weaponObjects = Resources.LoadAll("WeaponsResources/Weapons/Common");
            WeaponSO[] commonWeapons = new WeaponSO[weaponObjects.Length];
            weaponObjects.CopyTo(commonWeapons, 0);

            base.item = commonWeapons[Random.Range(0, weaponObjects.Length)];

        }
        else if (result >= 50 && result < 80) // 30% chance for uncommon weapon
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
