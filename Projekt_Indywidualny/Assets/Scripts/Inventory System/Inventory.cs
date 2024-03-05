using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one Inventory instance found!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public delegate void OnItemChanged();
    public static event OnItemChanged OnItemChangedCallback;

    private List<PassiveItem> passiveItems = new List<PassiveItem>(); 


    public List<PassiveItem> GetPassiveItemList()
    {
        return passiveItems;
    }
    public void AddPassiveItem(PassiveItem item)
    {
        passiveItems.Add(item);
        item.OnPickUp();
        OnItemChangedCallback?.Invoke();
    }

    public bool RemovePassiveItem(PassiveItem item)
    {
        OnItemChangedCallback?.Invoke();
        return passiveItems.Remove(item);

    }
}
