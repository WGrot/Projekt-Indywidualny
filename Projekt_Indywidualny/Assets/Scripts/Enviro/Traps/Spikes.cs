using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float force;
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Player Touched Spikes");
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            PlayerStatus.Instance.TakeDamage(damage);

            playerMovement.AddVelocity(force, true);

            AudioSource audiosurce = GetComponent<AudioSource>();
            audiosurce.Play();
        }
    }
}
