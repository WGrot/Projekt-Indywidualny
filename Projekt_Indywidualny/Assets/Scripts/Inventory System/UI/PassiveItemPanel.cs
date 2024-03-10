using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PassiveItemPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Image itemIcon;

    private PassiveItem displayedItem;

    private void OnEnable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback += ShowItemData;
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
        displayedItem = null;
        textBox.SetText("");
        itemIcon.sprite = null;

    }

}
