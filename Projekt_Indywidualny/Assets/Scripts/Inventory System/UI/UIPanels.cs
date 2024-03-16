using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIPanels : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiPanels;
    [SerializeField] private GameObject pauseScreen;
    private int activePanel;
    private bool isPanelOpened = false;
    private bool isGamePaused = false;
    private InputActions inputActions;


    #region passiveItemsPanelStuff
    private bool isInventoryDirty = false;
    private RectTransform rt;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject itemList;
    #endregion

    private void Start()
    {
        rt = itemList.GetComponent<RectTransform>();

    }
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.UI.OpenInventory.performed += OpenCloseInventoryOnIPressed;
        inputActions.UI.OpenPauseMenu.performed+= OpenClosePauseMenu;
    }

    private void OnEnable()
    {
        Inventory.OnItemChangedCallback += SetInventoryDirty;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        Inventory.OnItemChangedCallback -= SetInventoryDirty;
        inputActions.Disable();
    }

    public void OpenClosePauseMenu(InputAction.CallbackContext context)
    {
        if (isGamePaused)
        {
            GameStateManager.Instance.ResumeGame();
            pauseScreen.SetActive(false);
            isGamePaused = false;
        }
        else
        {

            pauseScreen.SetActive(true);
            ClosePanels();
            GameStateManager.Instance.PauseGame();
            isGamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            GameStateManager.Instance.ResumeGame();
            pauseScreen.SetActive(false);
            isGamePaused = false;
        }
    }

    public void QuitToMenu()
    {
        GameStateManager.Instance.ResumeGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
        if (manager != null)
        {
            Destroy(manager);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenCloseInventoryOnIPressed(InputAction.CallbackContext context)
    {
        if (isPanelOpened)
        {
            GameStateManager.Instance.ResumeGame();
            uiPanels[activePanel].SetActive(false);
            isPanelOpened = false;
        }
        else
        {

            uiPanels[0].SetActive(true);
            activePanel = 0;
            if (isInventoryDirty)
            {
                LoadPassiveItems();
            }
            GameStateManager.Instance.PauseGame();
            isPanelOpened = true;
        }

    }

    public void ClosePanels()
    {
        if (isPanelOpened)
        {
            GameStateManager.Instance.ResumeGame();
            uiPanels[activePanel].SetActive(false);
            isPanelOpened = false;
        }
    }

    public void SwitchToNextPanel()
    {
        uiPanels[activePanel].SetActive(false);
        if (activePanel + 1 < uiPanels.Count)
        {
            uiPanels[activePanel + 1].SetActive(true);
            activePanel += 1;
        }
        else
        {
            uiPanels[0].SetActive(true);
            activePanel = 0;
        }
    }
    public void SwitchToPreviousPanel()
    {
        uiPanels[activePanel].SetActive(false);
        if (activePanel -1 >= 0)
        {
            uiPanels[activePanel -1].SetActive(true);
            activePanel -= 1;
        }
        else
        {
            uiPanels[uiPanels.Count -1].SetActive(true);
            activePanel = uiPanels.Count - 1;
        }
    }


    #region passiveitemsPanel
    private void SetInventoryDirty()
    {
        isInventoryDirty = true;
    }

    public void LoadPassiveItems()
    {
        for (int i = 0; i < itemList.transform.childCount; i++)
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
        isInventoryDirty = false;
    }
    #endregion

}
