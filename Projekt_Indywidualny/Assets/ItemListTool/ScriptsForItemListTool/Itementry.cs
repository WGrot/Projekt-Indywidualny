using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class Itementry : MonoBehaviour
{
    private Item item;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI id;
    public TextMeshProUGUI ItemPoolsText;
    public Image itemImage;
    public TMP_Dropdown RarityDropDown;
    public TMP_Dropdown ItemPoolDropdown;
    public Toggle unlockedAtStart;
    public TMP_InputField newIdField;

    [Header("Changing Pools")]
    [SerializeField] private GameObject poolChangeList;
    [SerializeField] private GameObject PoolContainer;


    private List<PoollContainer> poolContainers = new List<PoollContainer>();

    PassiveItem itemCopy;
    WeaponSO weaponCopy;
    public void LoadItem(Item item)
    {
        this.item = item;
        NameText.text = item.name;
        itemImage.sprite = item.icon;
        RarityDropDown.value = (int)item.Rarity;
        unlockedAtStart.isOn = item.UnlockedAtStart;
        for (int i = 0; i < item.Pools.Count(); i++)
        {
            ItemPoolsText.text += (ItemPools)item.Pools.GetValue(i) + " ";
        }



        if (item is PassiveItem)
        {
            itemCopy = (PassiveItem)item;
            id.text = itemCopy.PassiveItemID.ToString();
            newIdField.text = id.text;
        }

        if (item is WeaponSO)
        {
            weaponCopy = (WeaponSO)item;
            id.text = weaponCopy.WeaponID.ToString();
            newIdField.text = id.text;
        }

    }

    public void Save()
    {
        item.Rarity = (Rarities)RarityDropDown.value;
        item.UnlockedAtStart = unlockedAtStart.isOn;

        if (newIdField.text != id.text && item is PassiveItem)
        {
            itemCopy.PassiveItemID = int.Parse(newIdField.text);
        }

        if (newIdField.text != id.text && item is WeaponSO)
        {
            weaponCopy.WeaponID = int.Parse(newIdField.text);
        }

    }

    public void LoadPoolsToBox()
    {
        poolContainers.Clear();
        foreach (Transform child in poolChangeList.transform)
        {
            Destroy(child.gameObject);
        }

        int totalNumberOfPools = Enum.GetNames(typeof(ItemPools)).Length;

        for (int i = 0; i < totalNumberOfPools; i++)
        {
            PoollContainer script = Instantiate(PoolContainer, poolChangeList.transform).GetComponent<PoollContainer>();
            poolContainers.Add(script);
            script.SetName(((ItemPools)i).ToString());
            if (item.Pools.Contains((ItemPools)i))
            {
                Debug.Log((ItemPools)i);
                script.SetToggleValue(true);
            }
        }
    }


    public void SaveChangedPools()
    {
        List<ItemPools> chosenPools = new List<ItemPools>();
        for (int i = 0; i < poolContainers.Count; i++)
        {

            PoollContainer poolContainer = poolContainers[i];
            if (poolContainer.GetToggleValue())
            {
                chosenPools.Add((ItemPools)i);
            }
        }

        item.Pools = chosenPools.ToArray();
    }
}
