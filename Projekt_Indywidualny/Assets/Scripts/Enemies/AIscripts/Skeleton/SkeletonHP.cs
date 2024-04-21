using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHP : EnemyHpBase
{
    [SerializeField] AudioClip takeDamageSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioSource audioSource;

    public override void Die()
    {
        audioSource.Stop();
        audioSource.clip = dieSound;
        audioSource.Play();
        
        base.Die();
    }

    public override void Start()
    {
        audioSource= GetComponent<AudioSource>();
        base.Start();
    }
    public override void TakeDamage(float damage)
    {
        if(audioSource.isPlaying != true)
        {
            audioSource.clip= takeDamageSound;
            audioSource.Play();
        }
        base.TakeDamage(damage);
    }
}
