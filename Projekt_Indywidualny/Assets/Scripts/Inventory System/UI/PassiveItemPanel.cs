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
        inputActions.UI.OpenInventory.performed += OpenCloseInventoryOnIPressed;
    }
    private void OnEnable()
    {
        Inventory.OnItemChangedCallback += SetDirty;
        inputActions.Enable();

    }

    private void OnDisable()
    {
        Inventory.OnItemChangedCallback -= SetDirty;
        inputActions.Disable();
    }

    private void LoadPassiveItems()
    {
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

}
