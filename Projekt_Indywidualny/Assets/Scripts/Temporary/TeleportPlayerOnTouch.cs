using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerOnTouch : MonoBehaviour
{
    [SerializeField] private GameObject DestinationPos;
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false; 
                other.transform.position = DestinationPos.transform.position;
                characterController.enabled = true; 
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource!= null)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
