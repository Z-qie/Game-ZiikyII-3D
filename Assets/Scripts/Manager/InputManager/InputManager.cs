// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Manager/InputManager/InputMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""021a662a-9da3-4f61-a80e-44fc1132f0d5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""eb4d6bf0-a6b3-46b4-95fa-e2fb65bce6e1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""48bc85ce-f0d3-426d-93c4-448964ec8fee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c0676105-e821-4881-975d-16d2584d9340"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseAiming"",
                    ""type"": ""Button"",
                    ""id"": ""b5f61aaf-e7cd-4f69-943b-65ea02af3239"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseZoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""742ae1fe-000b-404f-8992-11ec1ce40fb9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Columba"",
                    ""type"": ""Button"",
                    ""id"": ""245dc1c3-5125-464d-aa06-33d0e5583d30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Lepus"",
                    ""type"": ""Button"",
                    ""id"": ""665d115f-8d76-4fcd-ac49-ced48d9be659"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FirstSpell"",
                    ""type"": ""Button"",
                    ""id"": ""46b81cf0-5b7f-48c6-92cf-c550ba8442c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondSpell"",
                    ""type"": ""Button"",
                    ""id"": ""0843bd7c-a598-48e0-bd03-e4f553f9b721"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""8b1e9133-3218-4e87-802d-c716f560862b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""371a0f4e-938c-4a68-bae6-33b10bab3f9c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""85f26a21-9766-4ec4-abe2-d1c98fb4d14b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""fd0d6b3e-0ed2-4303-8273-f3aaac6cf04b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""7dd6cf11-28cf-4ef4-a572-bd08dc5fdce1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2d1ebdaf-505c-4c7c-8242-56b720040014"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21baa0c9-9e34-4499-a456-20ac80f0ab3d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22146f67-b4e9-4def-a54b-ccd2dde5c95b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6658ebb4-6dcf-4704-b61b-6664a826b377"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""56956bc5-3b68-4fa4-9d3d-e3b96c238ecd"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Columba"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""7e223937-a998-45ed-852c-241dd489255f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Columba"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""522b5107-4740-438f-94a7-949d856920f2"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Columba"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b3d7b7f7-aeb2-49c7-9659-4ae1840f1a3b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lepus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d45e802-fe29-4ce7-82a5-b85bb74f96c2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c81aa1e-9dae-41a8-b95c-348f542a1842"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_Move = m_InGame.FindAction("Move", throwIfNotFound: true);
        m_InGame_Dash = m_InGame.FindAction("Dash", throwIfNotFound: true);
        m_InGame_MousePosition = m_InGame.FindAction("MousePosition", throwIfNotFound: true);
        m_InGame_MouseAiming = m_InGame.FindAction("MouseAiming", throwIfNotFound: true);
        m_InGame_MouseZoom = m_InGame.FindAction("MouseZoom", throwIfNotFound: true);
        m_InGame_Columba = m_InGame.FindAction("Columba", throwIfNotFound: true);
        m_InGame_Lepus = m_InGame.FindAction("Lepus", throwIfNotFound: true);
        m_InGame_FirstSpell = m_InGame.FindAction("FirstSpell", throwIfNotFound: true);
        m_InGame_SecondSpell = m_InGame.FindAction("SecondSpell", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_Move;
    private readonly InputAction m_InGame_Dash;
    private readonly InputAction m_InGame_MousePosition;
    private readonly InputAction m_InGame_MouseAiming;
    private readonly InputAction m_InGame_MouseZoom;
    private readonly InputAction m_InGame_Columba;
    private readonly InputAction m_InGame_Lepus;
    private readonly InputAction m_InGame_FirstSpell;
    private readonly InputAction m_InGame_SecondSpell;
    public struct InGameActions
    {
        private @InputManager m_Wrapper;
        public InGameActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_InGame_Move;
        public InputAction @Dash => m_Wrapper.m_InGame_Dash;
        public InputAction @MousePosition => m_Wrapper.m_InGame_MousePosition;
        public InputAction @MouseAiming => m_Wrapper.m_InGame_MouseAiming;
        public InputAction @MouseZoom => m_Wrapper.m_InGame_MouseZoom;
        public InputAction @Columba => m_Wrapper.m_InGame_Columba;
        public InputAction @Lepus => m_Wrapper.m_InGame_Lepus;
        public InputAction @FirstSpell => m_Wrapper.m_InGame_FirstSpell;
        public InputAction @SecondSpell => m_Wrapper.m_InGame_SecondSpell;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Dash.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnDash;
                @MousePosition.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMousePosition;
                @MouseAiming.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMouseAiming;
                @MouseAiming.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMouseAiming;
                @MouseAiming.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMouseAiming;
                @MouseZoom.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMouseZoom;
                @MouseZoom.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMouseZoom;
                @MouseZoom.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMouseZoom;
                @Columba.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnColumba;
                @Columba.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnColumba;
                @Columba.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnColumba;
                @Lepus.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnLepus;
                @Lepus.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnLepus;
                @Lepus.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnLepus;
                @FirstSpell.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnFirstSpell;
                @FirstSpell.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnFirstSpell;
                @FirstSpell.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnFirstSpell;
                @SecondSpell.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnSecondSpell;
                @SecondSpell.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnSecondSpell;
                @SecondSpell.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnSecondSpell;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseAiming.started += instance.OnMouseAiming;
                @MouseAiming.performed += instance.OnMouseAiming;
                @MouseAiming.canceled += instance.OnMouseAiming;
                @MouseZoom.started += instance.OnMouseZoom;
                @MouseZoom.performed += instance.OnMouseZoom;
                @MouseZoom.canceled += instance.OnMouseZoom;
                @Columba.started += instance.OnColumba;
                @Columba.performed += instance.OnColumba;
                @Columba.canceled += instance.OnColumba;
                @Lepus.started += instance.OnLepus;
                @Lepus.performed += instance.OnLepus;
                @Lepus.canceled += instance.OnLepus;
                @FirstSpell.started += instance.OnFirstSpell;
                @FirstSpell.performed += instance.OnFirstSpell;
                @FirstSpell.canceled += instance.OnFirstSpell;
                @SecondSpell.started += instance.OnSecondSpell;
                @SecondSpell.performed += instance.OnSecondSpell;
                @SecondSpell.canceled += instance.OnSecondSpell;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    public interface IInGameActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseAiming(InputAction.CallbackContext context);
        void OnMouseZoom(InputAction.CallbackContext context);
        void OnColumba(InputAction.CallbackContext context);
        void OnLepus(InputAction.CallbackContext context);
        void OnFirstSpell(InputAction.CallbackContext context);
        void OnSecondSpell(InputAction.CallbackContext context);
    }
}
