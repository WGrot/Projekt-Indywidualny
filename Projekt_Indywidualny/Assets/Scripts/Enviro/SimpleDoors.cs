using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoors : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] AudioClip openingDoorsSound;
    [SerializeField] AudioClip closingDoorsSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    public void OpenDoors()
    {
        audioSource.clip = openingDoorsSound;
        audioSource.Play();
        animator.SetBool("DoorsAreOpened", true);
    }

    public void CloseDoors()
    {
        audioSource.clip = closingDoorsSound;
        audioSource.Play();
        animator.SetBool("DoorsAreOpened", false);
    }
}
