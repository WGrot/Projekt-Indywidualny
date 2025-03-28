using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    SaveData saveData;
    private static string SAVE_FOLDER;
    [SerializeField] bool shouldDeleteLockedItems = true;

    public delegate void OnItemUnlocked(Item item);
    public static event OnItemUnlocked OnItemUnlockedCallback;

    public delegate void OnWeaponUnlocked(Item item);
    public static event OnWeaponUnlocked OnWeaponUnlockedCallback;

    public delegate void OnBitsChanged();
    public static event OnBitsChanged OnBitsChangedCallback;

    #region Singleton
    public static SaveManager Instance { get; private set; }
    public SaveData SaveData { get => saveData; set => saveData = value; }

    private void Awake()
    {
        SAVE_FOLDER = Application.persistentDataPath + "/saves/";

        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one ItemPoolManager instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
            Init();
            LoadSavedData();
            InitiateItemPools();
            

        }
    }
    #endregion

    public void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        if(!File.Exists(SAVE_FOLDER+ "/unlockAtStartData.txt"))
        {
            Debug.Log("nie istnieje");
            
        }
        else
        {
            Debug.Log("istnieje");
        }
    }

    public void SaveSavedData()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/saves/save.txt", json);
        //File.WriteAllText(Application.persistentDataPath + "/saves/unlockAtStartData.txt", json);
    }

    public void LoadSavedData()
    {
        string saveString = File.ReadAllText(Application.persistentDataPath + "/saves/save.txt");
        saveData = JsonUtility.FromJson<SaveData>(saveString);
    }

    public void CreateUnlockAtStartFile(bool[] unlockedItems, bool[] unlockedWeapons)
    {
        UnlockAtStartData unlockData = new UnlockAtStartData(unlockedItems, unlockedWeapons);
        string json = JsonUtility.ToJson(unlockData);
        File.WriteAllText(Application.dataPath + "/saves/unlockAtStartData.txt", json);
        File.WriteAllText(Application.persistentDataPath + "/saves/unlockAtStartData.txt", json);
    }

    public void ResetSave()
    {
        string recoveryString = File.ReadAllText(Application.dataPath + "/saves/unlockAtStartData.txt");
        UnlockAtStartData unlockData = JsonUtility.FromJson<UnlockAtStartData>(recoveryString);
        saveData = new SaveData(unlockData.unlockedItems, unlockData.unlockedWeapons);
        SaveSavedData();
    }

    public void InitiateItemPools()
    {
        gameObject.AddComponent<ItemPoolsManager>();
        ItemPoolsManager.Instance.LoadPassiveItems();
        if (shouldDeleteLockedItems)
        {
            ItemPoolsManager.Instance.DeleteLockedItems(saveData.UnlockedPassiveItemsData);
        }
        ItemPoolsManager.Instance.AssignItemToPools();

        ItemPoolsManager.Instance.LoadWeapons();
        if (shouldDeleteLockedItems)
        {
            ItemPoolsManager.Instance.DeleteLockedWeapons(saveData.UnlockedWeaponsData);
        }
        ItemPoolsManager.Instance.AssignWeaponsToPools();
    }

    public void UnlockItem(int itemId)
    {
        if (saveData.CheckIfItemUnlocked(itemId))
        {
            return;
        }
        saveData.UnlockItem(itemId);
        SaveSavedData();
        if (OnItemUnlockedCallback != null)
        {
            OnItemUnlockedCallback(ItemPoolsManager.Instance.GetItemWithID(itemId));
        }
    }

    public void UnlockWeapon(int weaponId)
    {
        if (saveData.CheckIfWeaponUnlocked(weaponId))
        {
            return;
        }
        saveData.UnlockWeapon(weaponId);
        SaveSavedData();
        if (OnWeaponUnlockedCallback!= null)
        {
            OnWeaponUnlockedCallback(ItemPoolsManager.Instance.GetWeaponWithID(weaponId));
        }
    }

    public bool CheckIfItemUnlocked(int passiveItemId)
    {
        return saveData.CheckIfItemUnlocked(passiveItemId);
    }

    public bool CheckIfWeaponUnlocked(int weaponId)
    {
        return saveData.CheckIfWeaponUnlocked(weaponId);
    }

    public int GetCollectedBits()
    {
        return saveData.BitsCollected;
    }

    public void ChangeAmountOfCollectedBits(int amount)
    {
        saveData.ChangeAmountOfCollectedBits(amount);
        SaveSavedData();
        if (OnBitsChangedCallback!= null)
        {
            OnBitsChangedCallback();
        }
    }

}


