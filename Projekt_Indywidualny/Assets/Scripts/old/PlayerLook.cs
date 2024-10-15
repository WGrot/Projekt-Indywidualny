using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float mouseSpeed;

    private float savedMouseSpeed;

    private InputActions inputActions;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        UpdateMouseSpeed();
        GameStateManager.GameStateChangeCallback += UpdateMouseSpeed;

    }

    private void OnDisable()
    {
		GameStateManager.GameStateChangeCallback -= UpdateMouseSpeed;
		inputActions.Disable();
	}

    private void UpdateMouseSpeed()
    {
		savedMouseSpeed = PlayerPrefs.GetFloat("M_Sensitivity");
	}

	private void UpdateMouseSpeed(PauseState state)
	{
		savedMouseSpeed = PlayerPrefs.GetFloat("M_Sensitivity");
	}

	private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX = inputActions.Player_base.CameraRotation.ReadValue<Vector2>().x * mouseSpeed * (savedMouseSpeed/100) * Time.deltaTime;
        mouseY = inputActions.Player_base.CameraRotation.ReadValue<Vector2>().y * mouseSpeed * (savedMouseSpeed/100) * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

    }
}

