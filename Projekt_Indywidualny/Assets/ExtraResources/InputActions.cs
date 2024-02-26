//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/ExtraResources/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player_base"",
            ""id"": ""8c44b35a-a57e-4f18-8a70-4e2aa1421055"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""Value"",
                    ""id"": ""e6ef3740-1a0c-4617-8f2c-4450f68158a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""494d5a10-df6c-47af-a7b5-7c1ac2ecd555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraRotation"",
                    ""type"": ""Value"",
                    ""id"": ""85403333-f148-4d4d-83ff-5d9db0a39359"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Slam"",
                    ""type"": ""Button"",
                    ""id"": ""f6780b9d-988d-4d5c-8ebe-0d56fc90866a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""19be9a46-1058-417d-8696-b7e1f3070d75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a68dd6b2-187f-4f21-ab46-bf5c3ce08427"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8793952f-e7ab-4dff-9eeb-db2ae1b26ace"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d50f88db-88e7-4d75-92ef-b35c9aad5813"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7450e141-c8d9-4e09-bad9-289ddaeaf61d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ed11335d-ca33-4982-8260-3aad4b48de95"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""aa310a4b-9770-41e1-8823-b110a6fcc409"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72fd0919-a093-4333-b72a-e2c50c560b23"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c925836-252f-4493-82bc-e3414a690ad1"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""352c1018-0382-43e9-bba2-45ada3adae66"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_base
        m_Player_base = asset.FindActionMap("Player_base", throwIfNotFound: true);
        m_Player_base_HorizontalMovement = m_Player_base.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_Player_base_Jump = m_Player_base.FindAction("Jump", throwIfNotFound: true);
        m_Player_base_CameraRotation = m_Player_base.FindAction("CameraRotation", throwIfNotFound: true);
        m_Player_base_Slam = m_Player_base.FindAction("Slam", throwIfNotFound: true);
        m_Player_base_Dash = m_Player_base.FindAction("Dash", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player_base
    private readonly InputActionMap m_Player_base;
    private List<IPlayer_baseActions> m_Player_baseActionsCallbackInterfaces = new List<IPlayer_baseActions>();
    private readonly InputAction m_Player_base_HorizontalMovement;
    private readonly InputAction m_Player_base_Jump;
    private readonly InputAction m_Player_base_CameraRotation;
    private readonly InputAction m_Player_base_Slam;
    private readonly InputAction m_Player_base_Dash;
    public struct Player_baseActions
    {
        private @InputActions m_Wrapper;
        public Player_baseActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_Player_base_HorizontalMovement;
        public InputAction @Jump => m_Wrapper.m_Player_base_Jump;
        public InputAction @CameraRotation => m_Wrapper.m_Player_base_CameraRotation;
        public InputAction @Slam => m_Wrapper.m_Player_base_Slam;
        public InputAction @Dash => m_Wrapper.m_Player_base_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Player_base; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_baseActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_baseActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_baseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_baseActionsCallbackInterfaces.Add(instance);
            @HorizontalMovement.started += instance.OnHorizontalMovement;
            @HorizontalMovement.performed += instance.OnHorizontalMovement;
            @HorizontalMovement.canceled += instance.OnHorizontalMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @CameraRotation.started += instance.OnCameraRotation;
            @CameraRotation.performed += instance.OnCameraRotation;
            @CameraRotation.canceled += instance.OnCameraRotation;
            @Slam.started += instance.OnSlam;
            @Slam.performed += instance.OnSlam;
            @Slam.canceled += instance.OnSlam;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
        }

        private void UnregisterCallbacks(IPlayer_baseActions instance)
        {
            @HorizontalMovement.started -= instance.OnHorizontalMovement;
            @HorizontalMovement.performed -= instance.OnHorizontalMovement;
            @HorizontalMovement.canceled -= instance.OnHorizontalMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @CameraRotation.started -= instance.OnCameraRotation;
            @CameraRotation.performed -= instance.OnCameraRotation;
            @CameraRotation.canceled -= instance.OnCameraRotation;
            @Slam.started -= instance.OnSlam;
            @Slam.performed -= instance.OnSlam;
            @Slam.canceled -= instance.OnSlam;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
        }

        public void RemoveCallbacks(IPlayer_baseActions instance)
        {
            if (m_Wrapper.m_Player_baseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_baseActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_baseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_baseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_baseActions @Player_base => new Player_baseActions(this);
    public interface IPlayer_baseActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnSlam(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
