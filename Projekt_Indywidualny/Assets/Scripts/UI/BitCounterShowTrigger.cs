using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitCounterShowTrigger : MonoBehaviour, ILookable
{
    public delegate void OnLookedAtTrigger();
    public static event OnLookedAtTrigger OnLookedAtTriggerCallback;

    public void DoWhenLookedAt()
    {
        if (OnLookedAtTriggerCallback!= null)
        {
            OnLookedAtTriggerCallback();
        }
    }
}
