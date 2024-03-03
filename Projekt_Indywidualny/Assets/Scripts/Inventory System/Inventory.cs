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

    public delegate void OnItemChanged(Item item);
    public static event OnItemChanged onItemChangedCallback;

    public List<PassiveItem> passiveItems = new List<PassiveItem>(); 

    public void addPassiveItem(PassiveItem item)
    {
        passiveItems.Add(item);
        item.OnPickUp();
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback(item);
        }
    }

    public bool RemovePassiveItem(PassiveItem item)
    {
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback(item);
        }
        return passiveItems.Remove(item);

    }
}
