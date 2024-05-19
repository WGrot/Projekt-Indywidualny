using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionAndLooking : MonoBehaviour
{
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private float maxInteractionDistance;
    [SerializeField] private float maxLookDistance;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player_base.Interact.performed += CastInteractionBeam;
        
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }



    void Update()
    {
        CastLookingBeam();
    }

    private void CastInteractionBeam(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, maxInteractionDistance))
        {
            I_Interactable target = hit.transform.GetComponent<I_Interactable>();
            if (target != null)
            {
                target.InteractWithPlayer();
            }

        }
    }

    private void CastLookingBeam()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.forward, out hit, maxLookDistance))
        {
            ILookable target = hit.transform.GetComponent<ILookable>();
            if (target != null)
            {
                target.DoWhenLookedAt();
            }

        }
    }
}
