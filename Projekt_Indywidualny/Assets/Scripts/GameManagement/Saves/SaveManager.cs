using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    SaveData saveData;
    private static readonly string SAVE_FOLDER = Application.dataPath + "/saves/";

    #region Singleton
    public static SaveManager Instance { get; private set; }
    public SaveData SaveData { get => saveData; set => saveData = value; }

    private void Awake()
    {
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
    }

    public void SaveSavedData()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath + "/saves/save.txt", json);
    }

    public void LoadSavedData()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/saves/save.txt");
        saveData = JsonUtility.FromJson<SaveData>(saveString);
    }

    public void InitiateItemPools()
    {
        ItemPoolsManager.Instance.LoadPassiveItems();
        ItemPoolsManager.Instance.DeleteLockedItems(saveData.UnlockedPassiveItemsData);
        ItemPoolsManager.Instance.AssignItemToPools();

        ItemPoolsManager.Instance.LoadWeapons();
        ItemPoolsManager.Instance.DeleteLockedWeapons(saveData.UnlockedWeaponsData);
        ItemPoolsManager.Instance.AssignWeaponsToPools();
    }


}
