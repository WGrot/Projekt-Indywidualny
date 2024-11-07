using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Doors : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject viewBlock;
    private Collider doorCollider;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (OnPlayerEnter != null)
            {
                OnPlayerEnter.Invoke();
            }
        }
    }

    public void CloseDoor()
    {
        doorCollider = GetComponent<Collider>();
        doorCollider.enabled = false;
        closedDoor.SetActive(true);
        viewBlock.SetActive(false);
        
    }
    public void OpenDoor()
    {
        closedDoor.SetActive(false);
    }
}
