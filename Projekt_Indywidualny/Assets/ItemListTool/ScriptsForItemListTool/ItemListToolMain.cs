using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListToolMain : MonoBehaviour
{
    [SerializeField] GameObject ItemEntry;
    [SerializeField] GameObject ItemList;
    List<Itementry> entries= new List<Itementry>();
    public void LoadItems()
    {
        entries.Clear();
        foreach (Transform child in ItemList.transform)
        {
            Destroy(child.gameObject);
        }

        List<PassiveItem> items= new List<PassiveItem>();
        items = ItemPoolsManager.Instance.AllItemsList1;
        foreach (PassiveItem item in items)
        {
            GameObject entry = Instantiate(ItemEntry, ItemList.transform);
            Itementry script = entry.GetComponent<Itementry>();
            script.LoadItem(item);
            entries.Add(script);
        }
    }


    public void SaveChanges()
    {
        foreach(Itementry entry in entries)
        {
            entry.Save();
        }
    }
}
