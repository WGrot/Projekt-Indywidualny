using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float lifetime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("BulletImact", 0);
        Invoke("DestroyThis", lifetime);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
