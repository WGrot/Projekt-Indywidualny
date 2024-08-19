using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerminalButton : MonoBehaviour, I_Interactable
{
    public UnityEvent OnClick;
    public void InteractWithPlayer()
    {
        OnClick?.Invoke();
    }
}
