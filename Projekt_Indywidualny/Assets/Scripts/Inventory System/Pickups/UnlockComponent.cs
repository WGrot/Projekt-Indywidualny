using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockComponent : MonoBehaviour
{
    public void UnlockTargetWeapon(int id)
    {
        SaveManager.Instance.UnlockWeapon(id);
    }

    public void UnlockTargetPassiveItem(int id)
    {
        SaveManager.Instance.UnlockItem(id);
    }
}
