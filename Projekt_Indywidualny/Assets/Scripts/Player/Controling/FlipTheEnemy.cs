using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipTheEnemy : MonoBehaviour
{

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {

        FlipHitbox.OnFlippedActionCallback += Flip;
    }

    private void OnDisable()
    {

        FlipHitbox.OnFlippedActionCallback -= Flip;
    }

    void Flip()
    {
        StartCoroutine(AnimateFlip());
    }

    IEnumerator AnimateFlip()
    {
        animator.SetBool("isFlippin", true);
        yield return null;
        animator.SetBool("isFlippin", false);
    }
}
