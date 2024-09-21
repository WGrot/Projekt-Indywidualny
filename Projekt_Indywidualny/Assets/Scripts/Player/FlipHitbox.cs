using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipHitbox : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    [SerializeField] private float hitboxLifeTime;


    private void OnEnable()
    {
        FlipTheEnemy.OnFlippedActionCallback += ActiveHitbox;
    }

    private void OnDisable()
    {
        FlipTheEnemy.OnFlippedActionCallback -= ActiveHitbox;
    }

    void ActiveHitbox()
    {
        hitbox.SetActive(true);
        StartCoroutine(DesactivateHitbox());
    }


    IEnumerator DesactivateHitbox()
    {
        yield return new WaitForSeconds(hitboxLifeTime);
        hitbox.SetActive(false);
    }
}
