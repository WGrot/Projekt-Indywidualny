using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private InputActions inputActions;

    [Header("Gravity Parameters")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float freeFallLimit = 200f;

    [Header("Basic Parameters")]
    [SerializeField] private float moveSpeed;


    [Header("Jump Parameters")]
    [SerializeField] private float jumpPower;

    [SerializeField] private int jumpCount;

    [Header("Special Moves Parameters")]
    [SerializeField] private float slamAcceleration = 100f;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashMultipier;
    [SerializeField] private float dashCooldown = 1f;


    private bool isDashing = false;
    private bool canDash = true;
    private Vector3 completeMoveVector;
    private Vector2 horizontalInput;

    private int jumpCountPriv;
    private float verticalVelocity;

    private float speedStatvalue;
    private float staminaStatvalue;


    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player_base.Jump.performed += Jump;
        inputActions.Player_base.Slam.performed += Slam;
        inputActions.Player_base.Dash.performed += OnDashKeyPressed;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        #region handles CoyoteTime
        if (characterController.isGrounded != true)
        {

        }
        else
        {

            jumpCountPriv = jumpCount;
        }
        #endregion
        
        
        #region applies gravity
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
            
        }
        else if(verticalVelocity > -freeFallLimit)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        #endregion

        #region applies horizontal movement
        horizontalInput = Vector3.Normalize(inputActions.Player_base.HorizontalMovement.ReadValue<Vector2>()) ;
        if (!isDashing)
        {
            speedStatvalue = PlayerStatus.Instance.stats[(int)StatType.MoveSpeed].value;
            completeMoveVector = ((transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * moveSpeed * speedStatvalue + new Vector3(0, verticalVelocity, 0)) * Time.deltaTime;
        }



        #endregion
        characterController.Move(completeMoveVector);



    }



    private void Jump(InputAction.CallbackContext context)
    {
        if (jumpCountPriv > 0)
        {
            verticalVelocity = jumpPower;
            jumpCountPriv--;
        }
    }

    private void Slam(InputAction.CallbackContext context)
    {
        if (verticalVelocity > - freeFallLimit)
        {
            verticalVelocity -= slamAcceleration;
        }

    }


    private void OnDashKeyPressed(InputAction.CallbackContext context)
    {
        if (!isDashing && canDash && horizontalInput!= Vector2.zero)
        {
            StartCoroutine("Dash");
        }
    }
    private IEnumerator Dash()
    {
        staminaStatvalue = PlayerStatus.Instance.stats[(int)StatType.Stamina].value;
        isDashing = true;
        canDash = false;
        completeMoveVector = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * moveSpeed * dashMultipier * Time.deltaTime;
        yield return new WaitForSeconds(dashTime);
        verticalVelocity = -2f;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown * 1/staminaStatvalue);
        canDash = true;
    }

    public void AddVelocity(float force, bool resetsJumps)
    {
        if (resetsJumps)
        {
            jumpCountPriv = jumpCount;
        }
        verticalVelocity = force;
    }

}
