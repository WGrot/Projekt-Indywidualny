using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnlocker : MonoBehaviour
{
    public void UnlockTargetPassiveItem(int id)
    {
        SaveManager.Instance.UnlockItem(id);
    }
}
