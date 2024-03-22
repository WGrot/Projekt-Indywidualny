using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI prefixBox;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject itemList;
    private RectTransform rt;
    [SerializeField] GameObject itemSlotPrefab;

    private WeaponSO displayedItem;

    private void OnEnable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback += ShowItemData;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        rt = itemList.GetComponent<RectTransform>();
        LoadWeapons();
    }

    private void OnDisable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback -= ShowItemData;
    }

    private void ShowItemData(Item item)
    {
        displayedItem = (WeaponSO)item;
        textBox.SetText(item.description);
        itemIcon.sprite = item.icon;
        string prefixName = Inventory.Instance.GetPrefixWithIndex(Inventory.Instance.GetWeaponIndex((WeaponSO)item)).PrefixName; //this ungodly line finds prefix corresponding to clicked weapon
        prefixBox.SetText(prefixName);
    }
    /*
    public void DropItem()
    {
        if (displayedItem == null)
        {
            return;
        }
        Inventory.Instance.RemovePassiveItem(displayedItem);
        Vector3 dropPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 1, 0);
        GameObject dropInstance = Instantiate(dropPrefab, dropPos, Quaternion.identity);
        dropInstance.GetComponent<ItemPickup>().SetItem(displayedItem);
        displayedItem = null;
        textBox.SetText("");
        itemIcon.sprite = null;

    }
    */
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

