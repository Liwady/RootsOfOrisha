//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Controls/PlayerControls.inputactions
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
            ""name"": ""Gameplay"",
            ""id"": ""56b5fbca-519a-4e46-9821-1da9f402b280"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7bf5e573-d26f-40a2-8a56-717e9622c209"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""8ba0031f-c328-4f47-bfee-545553e2959e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TriggerAbility"",
                    ""type"": ""Button"",
                    ""id"": ""7448f3a4-db2e-4fd1-aaa8-301cfbb9d9c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchAbility"",
                    ""type"": ""Button"",
                    ""id"": ""768daa8b-129b-42ef-9386-53398d00c04a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleButton"",
                    ""type"": ""Button"",
                    ""id"": ""eb4d6cfc-3cdb-4870-9c17-df74e3236b8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Respawn"",
                    ""type"": ""Button"",
                    ""id"": ""ba8b450f-d4cd-4799-a944-30815d75d4b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""e83aed19-8b07-4492-ae33-47e25f0ecef6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3c8c3969-2205-4c5e-a5d4-938a037ed93a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""AD"",
                    ""id"": ""591ae1d0-88aa-4bd4-8239-77d0af829185"",
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
                    ""id"": ""3867a178-ddf4-43f0-a9a5-2db95e4131ee"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d84d1048-8d0c-499c-9f77-62701229fd8e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8fd61752-10cf-420e-affe-0f47c9b142f4"",
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
                    ""id"": ""061b3286-c468-4116-9341-c3c5acf65ade"",
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
                    ""id"": ""8641014e-89e7-479d-9b97-f4d52f986f84"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fe63075-5ad0-469b-9b33-e88c9d4ccac5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bface26-f60b-41ec-a924-fe4adb98c333"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a255ed9-e5d6-4a9f-87ac-e050fcc6d3cb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39d41cad-8f1d-4ee7-bf78-513da89f8fce"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f98b0381-f56c-4135-9ad4-0cbf0b01498c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d51cba9-c874-47ad-81a8-b0b7aa65b03c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bca9d69-5c02-4f60-a5a5-46372a49ced5"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a315b88-a602-4551-adaa-4a57a8636fc1"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca7055dd-7d50-4dbd-a770-709bec38343f"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_SwitchCharacter = m_Gameplay.FindAction("SwitchCharacter", throwIfNotFound: true);
        m_Gameplay_TriggerAbility = m_Gameplay.FindAction("TriggerAbility", throwIfNotFound: true);
        m_Gameplay_SwitchAbility = m_Gameplay.FindAction("SwitchAbility", throwIfNotFound: true);
        m_Gameplay_ToggleButton = m_Gameplay.FindAction("ToggleButton", throwIfNotFound: true);
        m_Gameplay_Respawn = m_Gameplay.FindAction("Respawn", throwIfNotFound: true);
        m_Gameplay_Grab = m_Gameplay.FindAction("Grab", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_SwitchCharacter;
    private readonly InputAction m_Gameplay_TriggerAbility;
    private readonly InputAction m_Gameplay_SwitchAbility;
    private readonly InputAction m_Gameplay_ToggleButton;
    private readonly InputAction m_Gameplay_Respawn;
    private readonly InputAction m_Gameplay_Grab;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @SwitchCharacter => m_Wrapper.m_Gameplay_SwitchCharacter;
        public InputAction @TriggerAbility => m_Wrapper.m_Gameplay_TriggerAbility;
        public InputAction @SwitchAbility => m_Wrapper.m_Gameplay_SwitchAbility;
        public InputAction @ToggleButton => m_Wrapper.m_Gameplay_ToggleButton;
        public InputAction @Respawn => m_Wrapper.m_Gameplay_Respawn;
        public InputAction @Grab => m_Wrapper.m_Gameplay_Grab;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @SwitchCharacter.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCharacter;
                @SwitchCharacter.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCharacter;
                @SwitchCharacter.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchCharacter;
                @TriggerAbility.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTriggerAbility;
                @TriggerAbility.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTriggerAbility;
                @TriggerAbility.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTriggerAbility;
                @SwitchAbility.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchAbility;
                @SwitchAbility.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchAbility;
                @SwitchAbility.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchAbility;
                @ToggleButton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleButton;
                @ToggleButton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleButton;
                @ToggleButton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleButton;
                @Respawn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRespawn;
                @Respawn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRespawn;
                @Respawn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRespawn;
                @Grab.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @SwitchCharacter.started += instance.OnSwitchCharacter;
                @SwitchCharacter.performed += instance.OnSwitchCharacter;
                @SwitchCharacter.canceled += instance.OnSwitchCharacter;
                @TriggerAbility.started += instance.OnTriggerAbility;
                @TriggerAbility.performed += instance.OnTriggerAbility;
                @TriggerAbility.canceled += instance.OnTriggerAbility;
                @SwitchAbility.started += instance.OnSwitchAbility;
                @SwitchAbility.performed += instance.OnSwitchAbility;
                @SwitchAbility.canceled += instance.OnSwitchAbility;
                @ToggleButton.started += instance.OnToggleButton;
                @ToggleButton.performed += instance.OnToggleButton;
                @ToggleButton.canceled += instance.OnToggleButton;
                @Respawn.started += instance.OnRespawn;
                @Respawn.performed += instance.OnRespawn;
                @Respawn.canceled += instance.OnRespawn;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSwitchCharacter(InputAction.CallbackContext context);
        void OnTriggerAbility(InputAction.CallbackContext context);
        void OnSwitchAbility(InputAction.CallbackContext context);
        void OnToggleButton(InputAction.CallbackContext context);
        void OnRespawn(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
    }
}
