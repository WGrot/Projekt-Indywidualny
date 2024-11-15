using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] UnityEvent OnPlayerEnter;
    [SerializeField] private bool disableAfterInvoking = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
            if (disableAfterInvoking)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
