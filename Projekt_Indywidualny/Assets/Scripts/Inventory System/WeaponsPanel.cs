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

    private PassiveItem displayedItem;

    private void OnEnable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback += ShowItemData;
        weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
    }

    private void OnDisable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback -= ShowItemData;
    }


    private void ShowItemData(Item item)
    {
        displayedItem = (PassiveItem)item;
        textBox.SetText(item.description);
        itemIcon.sprite = item.icon;
    }

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

}

