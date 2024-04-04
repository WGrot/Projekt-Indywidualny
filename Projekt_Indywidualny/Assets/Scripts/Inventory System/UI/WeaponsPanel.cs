using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WeaponsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI prefixBox;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private WeaponHolder weaponHolder;
    [SerializeField] private GameObject itemList;

    private RectTransform rt;
    [SerializeField] GameObject itemSlotPrefab;

    private WeaponSO displayedItem;

    private void OnEnable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback += ShowItemData;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponHolder>();
        rt = itemList.GetComponent<RectTransform>();
        LoadWeapons();
    }

    private void OnDisable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback -= ShowItemData;
        HideData();
    }

    private void ShowItemData(Item item)
    {
        displayedItem = (WeaponSO)item;
        textBox.SetText(item.description);
        weaponNameText.SetText(item.itemName);
        itemIcon.sprite = item.icon;
        string prefixName = Inventory.Instance.GetPrefixWithIndex(Inventory.Instance.GetWeaponIndex((WeaponSO)item)).PrefixName; //this ungodly line finds prefix corresponding to clicked weapon
        prefixBox.SetText(prefixName);
    }

    private void HideData()
    {
        displayedItem = null;
        textBox.SetText("");
        weaponNameText.SetText("");
        itemIcon.sprite = null;
        prefixBox.SetText("");
    }
    
    public void DropItem()
    {
        if (displayedItem == null)
        {
            return;
        }
        weaponHolder.DropWeaponAtIndex(Inventory.Instance.GetWeaponIndex(displayedItem));
        HideData();
    }
    
    public void LoadWeapons()
    {
        for (int i = 0; i < itemList.transform.childCount; i++)
        {
            Destroy(itemList.transform.GetChild(i).gameObject);
        }

        List<WeaponSO> weaponsList = Inventory.Instance.GetWeaponsList();
        foreach (WeaponSO item in weaponsList)
        {
            GameObject instance = Instantiate(itemSlotPrefab);
            instance.transform.SetParent(itemList.transform, false);
            instance.GetComponent<InventorySlot>().SetItem(item);

            rt.sizeDelta = new Vector2(rt.rect.width, 35 * weaponsList.Count);
        }
    }

}

