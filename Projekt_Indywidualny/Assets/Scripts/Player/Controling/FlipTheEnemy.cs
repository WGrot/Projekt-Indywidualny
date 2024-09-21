using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipTheEnemy : MonoBehaviour
{
    private InputActions inputActions;
    private Animator animator;
    [SerializeField] private float cooldown;
    private float timeSinceLastFlip;
    private bool canFlip = false;
    public delegate void FlippedAction();
    public static event FlippedAction OnFlippedActionCallback;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        timeSinceLastFlip = 0;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player_base.FlipTheEnemy.performed += Flip;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player_base.FlipTheEnemy.performed -= Flip;
    }


    public void Flip(InputAction.CallbackContext context)
    {
        if (timeSinceLastFlip > cooldown)
        {
            timeSinceLastFlip = 0;
            if (OnFlippedActionCallback!= null)
            {
                OnFlippedActionCallback();
            }
            StartCoroutine(AnimateFlip());

        }
        else
        {
            return;
        }
    }

    IEnumerator AnimateFlip()
    {
        animator.SetBool("isFlippin", true);
        yield return null;
        animator.SetBool("isFlippin", false);



    }


    
    void Update()
    {
        timeSinceLastFlip += Time.deltaTime;
    }
}
