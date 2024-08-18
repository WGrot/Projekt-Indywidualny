using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : ItemPickup
{
    [SerializeField] private bool isWeaponRandom = true;
    [SerializeField] private bool isPrefixRandom = true;
    [SerializeField] private WeaponSO weapon;
    [SerializeField] private WeaponPrefix prefix;
    [SerializeField] private AmmoData ammoData;
    [SerializeField] private bool shouldResetAmmo;

    public UnityEvent OnWeaponPickup;
    public override void Start()
    {

        if (isWeaponRandom)
        {
            base.item = ItemPoolsManager.Instance.GetRandomWeaponFromPool(base.pool);
            weapon = (WeaponSO)item;
        }
        if (shouldResetAmmo)
        {
            ammoData = new AmmoData(((WeaponSO)item).ClipSize, ((WeaponSO)item).MaxAmmo, ((WeaponSO)item).MaxAmmo);
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
            Inventory.Instance.AddAmmo(ammoData);
            Inventory.Instance.AddWeapon((WeaponSO)item);
            if(OnWeaponPickup != null)
            {
                OnWeaponPickup.Invoke();
            }

            base.InteractWithPlayer();
            Destroy(gameObject);

        }
        else
        {
            Debug.LogError("You configured something wrong, you used weapon pickup with item that is not a weapon");
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
