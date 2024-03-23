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
    [SerializeField] private GameObject dropPrefab;

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
        if (item is not PassiveItem)
        {
            Debug.LogError("There is something different than passive item in item list");
            return;
        }
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
        GameObject dropInstance= Instantiate(dropPrefab, dropPos, Quaternion.identity);
        dropInstance.GetComponent<ItemPickup>().SetItem(displayedItem);
        displayedItem = null;
        textBox.SetText("");
        itemIcon.sprite = null;

    }

}
