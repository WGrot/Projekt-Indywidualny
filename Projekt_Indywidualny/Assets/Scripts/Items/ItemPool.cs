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

    int mythicChance = 50;
    int rareChance = 150;
    int uncommonChance = 300;
    int commonChance = 500;

    public void AddItemToCorrectListList(Item item)
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
        bool foundItem = false;
        CheckForItemsInRarities();

        int rareChanceCombined = commonChance + uncommonChance + rareChance;
        int uncommonChanceCombined = commonChance + uncommonChance;
        int fullPool = commonChance + uncommonChance + rareChance + mythicChance;


        int randInt = Random.Range(1, fullPool);
        if (randInt < commonChance) //5% chance for Mythic Item
        {
            int rand = Random.Range(0, CommonItems.Count - 1);
            Debug.Log("Hit CommonItem" + CommonItems.Count + "   " + rand);
            result = CommonItems[rand];
            CommonItems.RemoveAt(rand);

        }
        else if (randInt >= commonChance && randInt < uncommonChanceCombined) // 15% chance for RareItem
        {
            int rand = Random.Range(0, UncommonItems.Count - 1);
            Debug.Log("Hit UnCommonItem" + UncommonItems.Count + "   " + rand);
            result = UncommonItems[rand];
            UncommonItems.RemoveAt(rand);

        }
        else if (randInt >= uncommonChanceCombined && randInt < rareChanceCombined) // 30% chance for Uncommon Item
        {
            int rand = Random.Range(0, RareItems.Count - 1);
            Debug.Log("Hit RareItem" + RareItems.Count + "   " + rand);
            result = RareItems[rand];
            RareItems.RemoveAt(rand);

        }
        else if (MythicItems.Count > 0)// 50% Chance for commonItem
        {
            int rand = Random.Range(0, MythicItems.Count - 1);
            Debug.Log("Hit MythicItem" + MythicItems.Count + "   " + rand);
            result = MythicItems[rand];
            MythicItems.RemoveAt(rand);

        }
        else
        {
            result = null;
            Debug.Log("Sharted");
        }
        return result;
    }

    public void CheckForItemsInRarities()
    {
        if (CommonItems.Count < 1)
        {
            commonChance = 0;
        }

        if (UncommonItems.Count < 1)
        {

            uncommonChance = 0;
        }

        if (RareItems.Count < 1)
        {

            rareChance = 0;
        }

        if (MythicItems.Count < 1)
        {

            mythicChance = 0;
        }




    }

}
