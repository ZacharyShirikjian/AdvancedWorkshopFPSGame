// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""96ccc015-5c83-4e9b-a1e7-e851dadc6d69"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""10ff3972-690c-411a-92bc-bcaafc4bff4b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""aae8fe8f-9515-4156-aa51-a0cfd188b320"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""199a07dc-ef4f-4f49-8d50-2fda4f6829bc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c34a9154-bcd9-4195-9550-9451c87a815b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""be9b4b8b-c852-4a05-848b-6a391ee5d36b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""a215006b-9991-4abd-aaae-c039d8d7bb0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""19e2fff6-701a-45a5-b255-4e24300b6e6c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ef283afc-e981-4841-9373-9c0279811aea"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4b66c3f0-9df7-4697-9cb6-5a14206920d0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6a57e6c5-e0d2-4997-9873-5575e4090c4d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b62b81fa-35a5-4bf7-b663-969af778f660"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""276a58db-9803-4824-977b-9833a50bc8db"",
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
                    ""id"": ""8a241008-baca-42e3-961c-d666f22a6d3d"",
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
                    ""id"": ""18c27081-9739-4e5c-b0f6-d1487bd8b7ff"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d54db28c-52db-47d0-b894-1c17f22e9814"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f9f1903-7baf-438e-9036-42f1ddd7338e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4dbd33b9-2448-4424-a0c4-96ba3e5765de"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f25a776b-43f3-42f3-a80a-c9fef61b022d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""711b9304-1ec0-429f-aaee-87705e895f3e"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""34b0c48b-a913-4842-a7b0-216a5452274c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""34f264f5-8ad7-4fc9-ad6d-517592298b1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""37035848-c865-4b24-a6b7-918e90ccba6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""77223fdc-1b02-427b-9696-9b6cb9ff52d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Adjust"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f778e95b-a27f-4c60-bf81-0677f3abb8ff"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePointer"",
                    ""type"": ""PassThrough"",
                    ""id"": ""458b9db2-84c1-4a16-ae48-5370f1fb1795"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""bc6ba83c-180d-4bf5-9709-0ddcdad94d44"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f12c2efd-6ec0-45d0-8b69-022fb077b9dc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""355902c5-01c7-4f44-982c-e6d925021ac3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6cc715cf-ccfc-40e9-8eaa-946a1fe781d1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2003781d-01fb-4490-ade0-f771a3ccc8a1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8ff0ac37-1c7b-4538-9442-aa1afb5da61a"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dafcdf81-3693-4dab-bddd-61bdb21da0f6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""885ecfbf-02c1-4d16-a028-e2755c19cfcb"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae54802d-296d-439f-ab85-9aa1c69455f1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Adjust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e91523ce-53d4-4b2f-9927-4f90b0b15784"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_MouseX = m_Movement.FindAction("MouseX", throwIfNotFound: true);
        m_Movement_MouseY = m_Movement.FindAction("MouseY", throwIfNotFound: true);
        m_Movement_Shoot = m_Movement.FindAction("Shoot", throwIfNotFound: true);
        m_Movement_Crouch = m_Movement.FindAction("Crouch", throwIfNotFound: true);
        m_Movement_Reload = m_Movement.FindAction("Reload", throwIfNotFound: true);
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_Select = m_Menus.FindAction("Select", throwIfNotFound: true);
        m_Menus_Navigate = m_Menus.FindAction("Navigate", throwIfNotFound: true);
        m_Menus_Pause = m_Menus.FindAction("Pause", throwIfNotFound: true);
        m_Menus_Cancel = m_Menus.FindAction("Cancel", throwIfNotFound: true);
        m_Menus_Adjust = m_Menus.FindAction("Adjust", throwIfNotFound: true);
        m_Menus_MousePointer = m_Menus.FindAction("MousePointer", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_MouseX;
    private readonly InputAction m_Movement_MouseY;
    private readonly InputAction m_Movement_Shoot;
    private readonly InputAction m_Movement_Crouch;
    private readonly InputAction m_Movement_Reload;
    public struct MovementActions
    {
        private @Controls m_Wrapper;
        public MovementActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @MouseX => m_Wrapper.m_Movement_MouseX;
        public InputAction @MouseY => m_Wrapper.m_Movement_MouseY;
        public InputAction @Shoot => m_Wrapper.m_Movement_Shoot;
        public InputAction @Crouch => m_Wrapper.m_Movement_Crouch;
        public InputAction @Reload => m_Wrapper.m_Movement_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @MouseX.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseX;
                @MouseY.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseY;
                @MouseY.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseY;
                @MouseY.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseY;
                @Shoot.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnShoot;
                @Crouch.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnCrouch;
                @Reload.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
                @MouseY.started += instance.OnMouseY;
                @MouseY.performed += instance.OnMouseY;
                @MouseY.canceled += instance.OnMouseY;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_Select;
    private readonly InputAction m_Menus_Navigate;
    private readonly InputAction m_Menus_Pause;
    private readonly InputAction m_Menus_Cancel;
    private readonly InputAction m_Menus_Adjust;
    private readonly InputAction m_Menus_MousePointer;
    public struct MenusActions
    {
        private @Controls m_Wrapper;
        public MenusActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_Menus_Select;
        public InputAction @Navigate => m_Wrapper.m_Menus_Navigate;
        public InputAction @Pause => m_Wrapper.m_Menus_Pause;
        public InputAction @Cancel => m_Wrapper.m_Menus_Cancel;
        public InputAction @Adjust => m_Wrapper.m_Menus_Adjust;
        public InputAction @MousePointer => m_Wrapper.m_Menus_MousePointer;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSelect;
                @Navigate.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Pause.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnPause;
                @Cancel.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnCancel;
                @Adjust.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnAdjust;
                @Adjust.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnAdjust;
                @Adjust.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnAdjust;
                @MousePointer.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnMousePointer;
                @MousePointer.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnMousePointer;
                @MousePointer.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnMousePointer;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Adjust.started += instance.OnAdjust;
                @Adjust.performed += instance.OnAdjust;
                @Adjust.canceled += instance.OnAdjust;
                @MousePointer.started += instance.OnMousePointer;
                @MousePointer.performed += instance.OnMousePointer;
                @MousePointer.canceled += instance.OnMousePointer;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface IMenusActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnAdjust(InputAction.CallbackContext context);
        void OnMousePointer(InputAction.CallbackContext context);
    }
}
