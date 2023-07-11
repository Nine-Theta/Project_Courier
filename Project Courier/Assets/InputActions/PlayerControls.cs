//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/InputActions/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""945965f3-279f-473e-ac65-180ec97d0040"",
            ""actions"": [
                {
                    ""name"": ""MoveNorth"",
                    ""type"": ""PassThrough"",
                    ""id"": ""21fc2eb8-5556-4b36-886a-46e6b315b4a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveEast"",
                    ""type"": ""PassThrough"",
                    ""id"": ""55316535-26d7-462e-992b-2c3e0f25dbb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveSouth"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b1d04906-fa06-4d89-9c76-3cef5fbaa6b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveWest"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7f493950-b0fe-4e05-83e6-47d8bb07e1e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6e89290a-ff71-459c-ae70-458344d93fa6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveNorth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa9d66ef-be6f-4855-964d-8e578f639e07"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveNorth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79ee573f-f24a-45d8-8e15-4195868a296e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveEast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7608e92-5f26-41e3-a2b2-6ad4283cc60f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveEast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a1dbf80-bfa1-4cbc-990b-ad315e49cf7f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveSouth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2f35ead-5417-499d-9dd8-901ec00dc6c3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveSouth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3acb41a4-56a0-4640-a1d5-696fdd043d3b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveWest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""423b0e02-2fee-4bf1-89fa-b840d9920230"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MoveWest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveNorth = m_Player.FindAction("MoveNorth", throwIfNotFound: true);
        m_Player_MoveEast = m_Player.FindAction("MoveEast", throwIfNotFound: true);
        m_Player_MoveSouth = m_Player.FindAction("MoveSouth", throwIfNotFound: true);
        m_Player_MoveWest = m_Player.FindAction("MoveWest", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MoveNorth;
    private readonly InputAction m_Player_MoveEast;
    private readonly InputAction m_Player_MoveSouth;
    private readonly InputAction m_Player_MoveWest;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveNorth => m_Wrapper.m_Player_MoveNorth;
        public InputAction @MoveEast => m_Wrapper.m_Player_MoveEast;
        public InputAction @MoveSouth => m_Wrapper.m_Player_MoveSouth;
        public InputAction @MoveWest => m_Wrapper.m_Player_MoveWest;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MoveNorth.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveNorth;
                @MoveNorth.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveNorth;
                @MoveNorth.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveNorth;
                @MoveEast.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveEast;
                @MoveEast.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveEast;
                @MoveEast.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveEast;
                @MoveSouth.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveSouth;
                @MoveSouth.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveSouth;
                @MoveSouth.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveSouth;
                @MoveWest.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveWest;
                @MoveWest.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveWest;
                @MoveWest.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveWest;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveNorth.started += instance.OnMoveNorth;
                @MoveNorth.performed += instance.OnMoveNorth;
                @MoveNorth.canceled += instance.OnMoveNorth;
                @MoveEast.started += instance.OnMoveEast;
                @MoveEast.performed += instance.OnMoveEast;
                @MoveEast.canceled += instance.OnMoveEast;
                @MoveSouth.started += instance.OnMoveSouth;
                @MoveSouth.performed += instance.OnMoveSouth;
                @MoveSouth.canceled += instance.OnMoveSouth;
                @MoveWest.started += instance.OnMoveWest;
                @MoveWest.performed += instance.OnMoveWest;
                @MoveWest.canceled += instance.OnMoveWest;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMoveNorth(InputAction.CallbackContext context);
        void OnMoveEast(InputAction.CallbackContext context);
        void OnMoveSouth(InputAction.CallbackContext context);
        void OnMoveWest(InputAction.CallbackContext context);
    }
}
