using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudVisualsEffects : MonoBehaviour
{
    [SerializeField] GameObject ParryVisualEffect;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        FlipHitbox.OnSuccessfulParryActionCallback += Test;
    }

    private void OnDisable()
    {
        FlipHitbox.OnSuccessfulParryActionCallback -= Test;
    }


    private void Test()
    {
        StartCoroutine(AnimateParryEffect());
    }
    private IEnumerator AnimateParryEffect()
    {
        animator.SetBool("isParrying", true);
        yield return null;
        animator.SetBool("isParrying", false);
    }


}
