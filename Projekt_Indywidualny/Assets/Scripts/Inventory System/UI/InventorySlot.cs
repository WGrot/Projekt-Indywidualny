using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    Item item;
    [SerializeField] TextMeshProUGUI text;

    public void ShowData()
    {
        text.SetText(item.itemName);
    }


}
