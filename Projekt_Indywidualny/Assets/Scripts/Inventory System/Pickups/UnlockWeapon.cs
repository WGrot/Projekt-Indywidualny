using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    public void UnlockTargetWeapon(int id)
    {
        SaveManager.Instance.UnlockWeapon(id);
    }
}
