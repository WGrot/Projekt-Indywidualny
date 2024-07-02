using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovePs : MonoBehaviour
{

    private InputActions inputActions;

    [Header("Gravity Parameters")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float freeFallLimit = 200f;

    [Header("Basic Parameters")]
    [SerializeField] private float moveForce;
    [SerializeField] private float stopForce;
    [SerializeField] private float maxMoveSpeed;


    [Header("Jump Parameters")]
    [SerializeField] private float jumpPower;

    [SerializeField] private int jumpCount;

    [Header("Special Moves Parameters")]
    [SerializeField] private float slamAcceleration = 100f;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashMultipier;
    [SerializeField] private float dashCooldown = 1f;

    [Header("Setup")]
    [SerializeField] GroundCheck groundCheck;

    private Rigidbody rb;

    private bool isDashing = false;
    private bool canDash = true;
    private Vector3 completeMoveVector;
    private Vector2 horizontalInput;

    private int jumpCountPriv = 10;
    private float verticalVelocity;

    private float speedStatvalue;
    private float staminaStatvalue;


    Vector3 horizontalInputReady;
    Vector3 horizontalVelocity;
    Vector3 targetHorizontalVelocity;

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
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        horizontalInput = inputActions.Player_base.HorizontalMovement.ReadValue<Vector2>().normalized;
        
        horizontalInputReady = new Vector3(horizontalInput.x, 0, horizontalInput.y);
        
        horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        targetHorizontalVelocity = transform.rotation * horizontalInputReady * maxMoveSpeed;

        Debug.Log((targetHorizontalVelocity - horizontalVelocity).magnitude);

        if (groundCheck.IsGrounded)
        {
            jumpCountPriv = jumpCount;
        }
        else
        {
            rb.AddForce(Vector3.down * gravity);
        }

        if(horizontalInput != Vector2.zero)
        {
            //if ((targetHorizontalVelocity - horizontalVelocity).magnitude > 1)
           // {
                rb.AddForce(transform.rotation * horizontalInputReady * moveForce);
           // }

        }
        else if(horizontalVelocity.magnitude > 2f)
        {
            rb.AddForce(- new Vector3(rb.velocity.x, 0, rb.velocity.z) * stopForce);

        }else if(horizontalVelocity.magnitude < 2f)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        


    }



    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(jumpCountPriv);
        if (jumpCountPriv > 0)
        {

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCountPriv--;
        }
    }

    private void Slam(InputAction.CallbackContext context)
    {
        if (verticalVelocity > -freeFallLimit)
        {
            verticalVelocity -= slamAcceleration;
        }

    }


    private void OnDashKeyPressed(InputAction.CallbackContext context)
    {
        if (!isDashing && canDash && horizontalInput != Vector2.zero)
        {
            StartCoroutine("Dash");
        }
    }
    private IEnumerator Dash()
    {
        staminaStatvalue = PlayerStatus.Instance.stats[2].value;
        isDashing = true;
        canDash = false;
        completeMoveVector = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * moveForce * dashMultipier * Time.deltaTime;
        yield return new WaitForSeconds(dashTime);
        verticalVelocity = -2f;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown * 1 / staminaStatvalue);
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
