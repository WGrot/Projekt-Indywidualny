using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicInteraction : MonoBehaviour, I_Interactable
{
    public UnityEvent OnInteracton;
    public void InteractWithPlayer()
    {
        if (OnInteracton != null)
        {
            OnInteracton.Invoke();
        }
    }
}
