using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassiveItemPanel : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject passiveItemPanel;

    private InputActions inputActions;

    private RectTransform rt;
    private bool isDirty = true;
    private bool isPanelOpened = false;

    private void Start()
    {
        rt = itemList.GetComponent<RectTransform>();
    }

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.UI.OpenInventory.performed += OpenInventory;
    }
    private void OnEnable()
    {
        Inventory.OnItemChangedCallback += SetDirty;

    }

    private void OnDisable()
    {
        Inventory.OnItemChangedCallback -= SetDirty;
    }

    private void LoadPassiveItems()
    {
        List<PassiveItem> passiveItemList = Inventory.Instance.GetPassiveItemList();
        foreach (PassiveItem item in passiveItemList)
        {
            GameObject instance = Instantiate(itemSlotPrefab);
            instance.transform.SetParent(itemList.transform, false);
            instance.GetComponentInChildren<TextMeshProUGUI>().SetText(item.itemName);
            rt.sizeDelta = new Vector2(rt.rect.width, 35 * passiveItemList.Count);
        }
        isDirty = false;
    }

    private void SetDirty()
    {
        isDirty = true;
    }


    public void OpenInventory(InputAction.CallbackContext context)
    {
        Debug.Log("OpenedInventory");
        if (isPanelOpened)
        {
            passiveItemPanel.SetActive(false);
            isPanelOpened = false;
        }
        else
        {
            passiveItemPanel.SetActive(true);
            if (isDirty)
            {
                LoadPassiveItems();
            }
            isPanelOpened=true;
        }

    }

}
