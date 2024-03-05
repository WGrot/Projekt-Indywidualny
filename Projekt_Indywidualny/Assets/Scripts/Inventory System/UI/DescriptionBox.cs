using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Image itemIcon;

    private void OnEnable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback += ShowData;
    }

    private void OnDisable()
    {
        InventorySlot.OnItemSelectedInInventoryCallback -= ShowData;
    }
    private void ShowData(Item item)
    {
        textBox.SetText(item.description);
        itemIcon.sprite = item.icon;
    }
}
