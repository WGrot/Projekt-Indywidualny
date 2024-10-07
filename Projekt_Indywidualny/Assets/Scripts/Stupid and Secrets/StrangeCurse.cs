using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeCurse : MonoBehaviour, ILookable, I_Parryable
{

    [SerializeField] private float cameraShakePower;
    [SerializeField] private float damageCooldown;
    private AudioSource audioSource;
    private bool isPlayerTakinDamage = false;
    private bool isFlipped = false;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void DoWhenLookedAt()
    {
        if (!isFlipped)
        {
            return;
        }
        CameraShake.Instance.ShakeCamera(cameraShakePower, 0.1f);
        if (!isPlayerTakinDamage)
        {
            StartCoroutine(DealDamageCoroutine());
        }
    }

    public bool Parry()
    {
        isFlipped= true;
        audioSource.Play();
        return false;
    }


    IEnumerator DealDamageCoroutine()
    {
        isPlayerTakinDamage = true;
        PlayerStatus.Instance.TakeDamage(2f);
        yield return new WaitForSeconds(damageCooldown);
        isPlayerTakinDamage = false;
    }
}
