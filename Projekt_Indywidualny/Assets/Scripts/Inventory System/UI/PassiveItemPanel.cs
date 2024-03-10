using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PassiveItemPanel : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject passiveItemPanel;

    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Image itemIcon;

    private InputActions inputActions;

    private RectTransform rt;
    private bool isDirty = true;
    private bool isPanelOpened = false;
    private PassiveItem displayedItem;

    private void Start()
    {
        rt = itemList.GetComponent<RectTransform>();
    }

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.UI.OpenInventory.performed += OpenCloseInventoryOnIPressed;
    }
    private void OnEnable()
    {
        Inventory.OnItemChangedCallback += SetDirty;
        InventorySlot.OnItemSelectedInInventoryCallback += ShowItemData;
        inputActions.Enable();

    }

    private void OnDisable()
    {
        Inventory.OnItemChangedCallback -= SetDirty;
        InventorySlot.OnItemSelectedInInventoryCallback -= ShowItemData;
        inputActions.Disable();
    }

    private void LoadPassiveItems()
    {
        for (int i = 0; i< itemList.transform.childCount; i++ )
        {
            Destroy(itemList.transform.GetChild(i).gameObject);
        }

        List<PassiveItem> passiveItemList = Inventory.Instance.GetPassiveItemList();
        foreach (PassiveItem item in passiveItemList)
        {
            GameObject instance = Instantiate(itemSlotPrefab);
            instance.transform.SetParent(itemList.transform, false);
            instance.GetComponent<InventorySlot>().SetItem(item);

            rt.sizeDelta = new Vector2(rt.rect.width, 35 * passiveItemList.Count);
        }
        isDirty = false;
    }

    private void SetDirty()
    {
        isDirty = true;
    }


    public void OpenCloseInventoryOnIPressed(InputAction.CallbackContext context)
    {
        if (isPanelOpened)
        {
            GameStateManager.Instance.ResumeGame();
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
            GameStateManager.Instance.PauseGame();
            isPanelOpened=true;
        }

    }

    public void CloseInventory()
    {
        if (isPanelOpened)
        {
            GameStateManager.Instance.ResumeGame();
            passiveItemPanel.SetActive(false);
            isPanelOpened = false;
        }
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
        LoadPassiveItems();
        displayedItem = null;
        textBox.SetText("");
        itemIcon.sprite = null;

    }

}
