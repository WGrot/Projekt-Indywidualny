using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHP : EnemyHpBase
{
    [SerializeField] AudioClip takeDamageSound;
    AudioSource audioSource;

    [Header("AnimationsConfiguration")]
    [SerializeField] private GameObject deathProp;

    public override void Die()
    {
        Instantiate(deathProp, transform.position, Quaternion.identity);
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
