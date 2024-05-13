using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioClip playerHurtClip;


    public void OnEnable()
    {
        PlayerStatus.OnPlayerTakeDamageCallback += PlayHurtSound;
    }

    public void OnDisable()
    {
        PlayerStatus.OnPlayerTakeDamageCallback -= PlayHurtSound;
    }

    public void PlayHurtSound()
    {
        audioSource.clip = playerHurtClip;
        audioSource.Play();
    }
}
