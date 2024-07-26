using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIPanels : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiPanels;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject InGameHUD;

    [SerializeField] private AudioSource panelSwapAS;
    [SerializeField] private AudioSource panelOpenAS;
    [SerializeField] private AudioSource panelCloseAS;

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
        inputActions.UI.OpenMapMenu.performed += OpenMapOnTabPressed;
        inputActions.UI.OpenPauseMenu.performed+= OpenClosePauseMenu;
        inputActions.UI.SwitchToNextPanel.performed += _ => SwitchToNextPanel();
        inputActions.UI.SwitchToPreviousPanel.performed += _ => SwitchToPreviousPanel();
    }

    private void OnEnable()
    {
        Inventory.OnItemChangedCallback += SetInventoryDirty;
        PlayerStatus.OnPlayerDieCallback += ShowEndScreen;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        Inventory.OnItemChangedCallback -= SetInventoryDirty;
        PlayerStatus.OnPlayerDieCallback -= ShowEndScreen;
        inputActions.Disable();
    }
    public void ShowEndScreen()
    {
        deathScreen.SetActive(true);
        InGameHUD.SetActive(false);
        GameStateManager.Instance.PauseGame();
        isGamePaused = true;
    }

    public void ResumeGame() //Nie usuwaæ
    {
        if (isGamePaused)
        {
            GameStateManager.Instance.ResumeGame();
            pauseScreen.SetActive(false);
            InGameHUD.SetActive(true);
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

    public void ReturnToNexus()
    {
        GameStateManager.Instance.ResumeGame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
        if (manager != null)
        {
            Destroy(manager);
        }
        SceneManager.LoadScene("Nexus");
    }

    #region Opening Panels
    public void OpenClosePauseMenu(InputAction.CallbackContext context)
    {
        if (PlayerStatus.Instance.IsPlayerDead)
        {
            return;
        }

        if (isGamePaused)
        {
            ClosePanels();
            /*
            GameStateManager.Instance.ResumeGame();
            pauseScreen.SetActive(false);
            isGamePaused = false;
            InGameHUD.SetActive(true);
            */
        }
        else
        {
            panelOpenAS.Play();
            pauseScreen.SetActive(true);
            InGameHUD.SetActive(false);
            ClosePanels(false);
            GameStateManager.Instance.PauseGame();
            isGamePaused = true;
        }
    }
    public void OpenCloseInventoryOnIPressed(InputAction.CallbackContext context)
    {
        if (isPanelOpened)
        {
            ClosePanels();
            /*
            GameStateManager.Instance.ResumeGame();
            uiPanels[activePanel].SetActive(false);
            InGameHUD.SetActive(true);
            isPanelOpened = false;
            */
        }
        else if(GameStateManager.Instance.GetPauseState() == PauseState.Running)
        {
            panelOpenAS.Play();
            uiPanels[0].SetActive(true);
            InGameHUD.SetActive(false);
            activePanel = 0;
            LoadPassiveItems();
            GameStateManager.Instance.PauseGame();
            isPanelOpened = true;
        }

    }

    public void OpenMapOnTabPressed(InputAction.CallbackContext context)
    {
        if (isPanelOpened)
        {
            ClosePanels();
            /*
            GameStateManager.Instance.ResumeGame();
            uiPanels[activePanel].SetActive(false);
            InGameHUD.SetActive(true);
            isPanelOpened = false;
            */
        }
        else if (GameStateManager.Instance.GetPauseState() == PauseState.Running)
        {
            panelOpenAS.Play();
            uiPanels[3].SetActive(true);
            InGameHUD.SetActive(false);
            activePanel = 3;
            GameStateManager.Instance.PauseGame();
            isPanelOpened = true;
        }

    }
    #endregion

    #region Closing Panels

    public void ClosePanels(bool showHud)
    {
        if (isPanelOpened)
        {
            panelCloseAS.Play();
            GameStateManager.Instance.ResumeGame();
            uiPanels[activePanel].SetActive(false);
            InGameHUD.SetActive(showHud);
            isPanelOpened = false;
        }
    }

    public void ClosePanels()
    {
        if (isPanelOpened)
        {
            panelCloseAS.Play();
            GameStateManager.Instance.ResumeGame();
            uiPanels[activePanel].SetActive(false);
            InGameHUD.SetActive(true);
            isPanelOpened = false;
        }
    }
    #endregion

    #region Switching Panels
    public void SwitchToNextPanel()
    {
        if (!isPanelOpened)
        {
            return;
        }
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
        panelSwapAS.Play();
    }
    public void SwitchToPreviousPanel()
    {
        if (!isPanelOpened)
        {
            return;
        }
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
        panelSwapAS.Play();
    }
    #endregion

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
