using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockTerminal : MonoBehaviour
{
    [SerializeField] private int amountAvailableToBuy;
    [SerializeField] private List<PassiveItem> passiveItemsToPurchase;
    [SerializeField] private List<WeaponSO> weaponsToPurchase;


    private List<Item> itemsAvailableToPurchase = new List<Item>();
    private Item displayedItem;
    private int displayedItemIndex;

    [SerializeField] TextMeshPro costText;
    [SerializeField] SpriteRenderer itemSprite;
    [SerializeField] Sprite buyOutSprite;



    private void Start()
    {
        SaveManager.Instance.ResetSave();
        LoadItems();
        ShowDisplayedItem();

    }

    public void SwitchDisplayedItem(int value)
    {
        if(displayedItem == null)
        {
            return;
        }
        Debug.Log(displayedItemIndex + "  " + value);
        if (displayedItemIndex + value < 0)
        {
            displayedItemIndex = amountAvailableToBuy-1;
        }
        else if (displayedItemIndex + value > amountAvailableToBuy-1)
        {
            displayedItemIndex = 0;
        }
        else
        {
            displayedItemIndex += value;
        }

        displayedItem = itemsAvailableToPurchase[displayedItemIndex];
        ShowDisplayedItem();
    }

    void ShowDisplayedItem()
    {
        if(displayedItem != null)
        {
            itemSprite.sprite = displayedItem.icon;
            costText.text = displayedItem.UnlockCost.ToString();
        }
        else
        {
            itemSprite.sprite = buyOutSprite;
            costText.text = "69";
        }

    }

    public void UnlockDisplayedItem()
    {
        if (displayedItem == null)
        {
            return;
        }

        if (displayedItem is PassiveItem)
        {
            PassiveItem displayedPassiveItem = (PassiveItem)displayedItem;
            SaveManager.Instance.UnlockItem(displayedPassiveItem.PassiveItemID);
        }

        if(displayedItem is WeaponSO)
        {
            WeaponSO displayedWeapon = (WeaponSO)displayedItem;
            SaveManager.Instance.UnlockWeapon(displayedWeapon.WeaponID);
        }
        LoadItems();
        ShowDisplayedItem();
    }

    void LoadItems()
    {
        for (int i = weaponsToPurchase.Count - 1; i >= 0; i--)
        {
            WeaponSO weapon = weaponsToPurchase[i];
            if (SaveManager.Instance.CheckIfWeaponUnlocked(weapon.WeaponID))
            {
                weaponsToPurchase.RemoveAt(i);
            }
        }

        for (int i = passiveItemsToPurchase.Count - 1; i >= 0; i--)
        {
            PassiveItem item = passiveItemsToPurchase[i];
            if (SaveManager.Instance.CheckIfItemUnlocked(item.PassiveItemID))
            {
                passiveItemsToPurchase.RemoveAt(i);
            }
        }
        itemsAvailableToPurchase.Clear();

        int passiveItemsAmount = 4;
        if (passiveItemsToPurchase.Count < passiveItemsAmount)
        {
            passiveItemsAmount = passiveItemsToPurchase.Count;
        }
        itemsAvailableToPurchase.AddRange(passiveItemsToPurchase.GetRange(0, passiveItemsAmount));

        int weaponsAmount = 5 - passiveItemsAmount;
        if (weaponsToPurchase.Count < weaponsAmount)
        {
            weaponsAmount = weaponsToPurchase.Count;
        }
        itemsAvailableToPurchase.AddRange(weaponsToPurchase.GetRange(0, weaponsAmount));

        amountAvailableToBuy = passiveItemsAmount + weaponsAmount;

        if (amountAvailableToBuy != 0)
        {
            displayedItem = itemsAvailableToPurchase[0];
            displayedItemIndex = 0;
        }
        else
        {
            displayedItem = null;
            displayedItemIndex = 0;
        }

    }
}
