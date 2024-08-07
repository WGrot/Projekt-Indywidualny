using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPool
{
    List<PassiveItem> CommonItems = new List<PassiveItem>();
    List<PassiveItem> UncommonItems = new List<PassiveItem>();
    List<PassiveItem> RareItems = new List<PassiveItem>();
    List<PassiveItem> MythicItems = new List<PassiveItem>();
    List<PassiveItem> SpecialItems = new List<PassiveItem>();


    List<WeaponSO> CommonWeapons = new List<WeaponSO>();
    List<WeaponSO> UncommonWeapons = new List<WeaponSO>();
    List<WeaponSO> RareWeapons = new List<WeaponSO>();
    List<WeaponSO> MythicWeapons = new List<WeaponSO>();
    List<WeaponSO> SpecialWeapons = new List<WeaponSO>();

    int mythicItemChance = 50;
    int rareItemChance = 150;
    int uncommonItemChance = 300;
    int commonItemChance = 500;

    int mythicWeaponChance = 50;
    int rareWeaponChance = 150;
    int uncommonWeaponChance = 300;
    int commonWeaponChance = 500;

    public void AddItemToCorrectList(Item item)
    {
        if (item is PassiveItem)
        {
            switch (item.Rarity)
            {
                case Rarities.Common:
                    CommonItems.Add((PassiveItem)item);
                    break;
                case Rarities.Uncommon:
                    UncommonItems.Add((PassiveItem)item);
                    break;
                case Rarities.Rare:
                    RareItems.Add((PassiveItem)item);
                    break;
                case Rarities.Mythic:
                    MythicItems.Add((PassiveItem)item);
                    break;
                case Rarities.Special:
                    SpecialItems.Add((PassiveItem)item);
                    break;
            }
        }
        else if (item is WeaponSO)
        {
            switch (item.Rarity)
            {
                case Rarities.Common:
                    CommonWeapons.Add((WeaponSO)item);
                    break;
                case Rarities.Uncommon:
                    UncommonWeapons.Add((WeaponSO)item);
                    break;
                case Rarities.Rare:
                    RareWeapons.Add((WeaponSO)item);
                    break;
                case Rarities.Mythic:
                    MythicWeapons.Add((WeaponSO)item);
                    break;
                case Rarities.Special:
                    SpecialWeapons.Add((WeaponSO)item);
                    break;
            }
        }
    }

    public PassiveItem GetRandomPassiveItem()
    {
        PassiveItem result;
        CheckForItemsInRarities();

        int rareChanceCombined = commonItemChance + uncommonItemChance + rareItemChance;
        int uncommonChanceCombined = commonItemChance + uncommonItemChance;
        int fullPool = commonItemChance + uncommonItemChance + rareItemChance + mythicItemChance;


        int randInt = Random.Range(1, fullPool);
        if (randInt < commonItemChance) //5% chance for Mythic Item
        {
            int rand = Random.Range(0, CommonItems.Count - 1);
            result = CommonItems[rand];
            CommonItems.RemoveAt(rand);

        }
        else if (randInt >= commonItemChance && randInt < uncommonChanceCombined) // 15% chance for RareItem
        {
            int rand = Random.Range(0, UncommonItems.Count - 1);
            result = UncommonItems[rand];
            UncommonItems.RemoveAt(rand);

        }
        else if (randInt >= uncommonChanceCombined && randInt < rareChanceCombined) // 30% chance for Uncommon Item
        {
            int rand = Random.Range(0, RareItems.Count - 1);
            result = RareItems[rand];
            RareItems.RemoveAt(rand);

        }
        else if (MythicItems.Count > 0)// 50% Chance for commonItem
        {
            int rand = Random.Range(0, MythicItems.Count - 1);
            result = MythicItems[rand];
            MythicItems.RemoveAt(rand);

        }
        else
        {
            result = null;
        }
        return result;
    }




    public WeaponSO GetRandomWeapon()
    {
        WeaponSO result;

        int rareChanceCombined = commonWeaponChance + uncommonWeaponChance + rareWeaponChance;
        int uncommonChanceCombined = commonWeaponChance + uncommonWeaponChance;
        int fullPool = commonWeaponChance + uncommonWeaponChance + rareWeaponChance + mythicWeaponChance;


        int randInt = Random.Range(1, fullPool);
        if (randInt < commonWeaponChance)
        {
            int rand = Random.Range(0, CommonWeapons.Count - 1);
            result = CommonWeapons[rand];


        }
        else if (randInt >= commonWeaponChance && randInt < uncommonChanceCombined)
        {
            int rand = Random.Range(0, UncommonWeapons.Count - 1);
            result = UncommonWeapons[rand];


        }
        else if (randInt >= uncommonChanceCombined && randInt < rareChanceCombined)
        {
            int rand = Random.Range(0, RareWeapons.Count - 1);
            result = RareWeapons[rand];


        }
        else// 50% Chance for commonItem
        {
            int rand = Random.Range(0, MythicWeapons.Count - 1);
            result = MythicWeapons[rand];

             
        }
        return result;
    }

    public void CheckForItemsInRarities()
    {
        if (CommonItems.Count < 1)
        {
            commonItemChance = 0;
        }

        if (UncommonItems.Count < 1)
        {

            uncommonItemChance = 0;
        }

        if (RareItems.Count < 1)
        {

            rareItemChance = 0;
        }

        if (MythicItems.Count < 1)
        {

            mythicItemChance = 0;
        }
    }


}
