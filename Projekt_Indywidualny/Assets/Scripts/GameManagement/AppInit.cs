using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInit : MonoBehaviour
{
    private static string SAVE_FOLDER;
    [SerializeField] private string menuSceneName;
    private TextAsset loadedUnlockFile;
    private void Awake()
    {
        SAVE_FOLDER = Application.persistentDataPath + "/saves/";
        Init();
        SceneManager.LoadScene(menuSceneName);
    }

    public void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        if (!File.Exists(SAVE_FOLDER + "/unlockAtStartData.txt"))
        {
            Debug.Log("data nie istnieje tworzymy");
            loadedUnlockFile = (TextAsset)Resources.Load("SaveSetup/unlockAtStartData");
            string data = loadedUnlockFile.text;
            File.WriteAllText(Application.persistentDataPath + "/saves/unlockAtStartData.txt", data);

        }

        if (!File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            Debug.Log("save nie istnieje tworzymy");
            //string recoveryString = File.ReadAllText(Application.dataPath + "/saves/unlockAtStartData.txt");
            UnlockAtStartData unlockData = JsonUtility.FromJson<UnlockAtStartData>(loadedUnlockFile.text);
            SaveData saveData = new SaveData(unlockData.unlockedItems, unlockData.unlockedWeapons);

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath + "/saves/save.txt", json);
            

        }
    }
}
