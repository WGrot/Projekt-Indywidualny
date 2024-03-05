using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private TextMeshProUGUI itemNameText;

    public delegate void OnItemSelectedInInventory(Item item);
    public static event OnItemSelectedInInventory OnItemSelectedInInventoryCallback;

    public void ShowDescription()
    {
        if (OnItemSelectedInInventoryCallback != null)
        {
            OnItemSelectedInInventoryCallback(item);
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        itemNameText.SetText(item.itemName);
    }


}
