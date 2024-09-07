using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject colliders;
    [SerializeField] ParticleSystem shockwaveEffect;
    [SerializeField] private float force;

    [SerializeField] private float timeBuffour = 0.1f;
    private float currentTimeBuffour;

    private void Update()
    {
        float scaleFactor = Mathf.Pow(shockwaveEffect.time / shockwaveEffect.main.startLifetime.constant, 0.5f) * shockwaveEffect.main.startSize.constant / 6f;
        colliders.transform.localScale = new Vector3(scaleFactor, colliders.transform.localScale.y, scaleFactor);
        currentTimeBuffour -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (currentTimeBuffour > 0)
            {
                return;
            }
            currentTimeBuffour = timeBuffour;
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            PlayerStatus.Instance.TakeDamage(damage);

            playerMovement.AddVelocity(force, true);

        }
    }



}
