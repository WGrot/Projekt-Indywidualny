using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyNotificator : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("You Destroyed something");
    }
}
