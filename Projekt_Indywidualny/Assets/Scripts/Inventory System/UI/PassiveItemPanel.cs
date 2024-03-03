using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassiveItemPanel : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] GameObject itemSlotPrefab;
    private RectTransform rt;
    List<GameObject> list = new List<GameObject>();

    private void Start()
    {
        rt = itemList.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        Inventory.onItemChangedCallback += AddItemToList;
    }

    private void OnDisable()
    {
        Inventory.onItemChangedCallback -= AddItemToList;
    }

    private void AddItemToList(Item item)
    {

        GameObject instance = Instantiate(itemSlotPrefab);
        instance.transform.SetParent(itemList.transform, false);
        instance.GetComponentInChildren<TextMeshProUGUI>().SetText(item.itemName);
        list.Add(instance);
        rt.sizeDelta = new Vector2(rt.rect.width, 35* list.Count);
    }



}
