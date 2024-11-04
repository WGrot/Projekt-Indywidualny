using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bitPickup : MonoBehaviour
{
    [SerializeField] int amount = 1;
    [SerializeField] AudioClip PickupSound;
    [SerializeField] AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = PickupSound;
            audioSource.Play();
            SaveManager.Instance.ChangeAmountOfCollectedBits(1);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyBitAfterPickup());
        }
    }

    private IEnumerator DestroyBitAfterPickup()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
