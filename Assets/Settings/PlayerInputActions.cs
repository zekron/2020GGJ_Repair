// GENERATED AUTOMATICALLY FROM 'Assets/Settings/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""InWelcome"",
            ""id"": ""cb6c2606-269b-4de4-9324-ec02324ab3e2"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""b461cf4f-f862-43c1-8b67-eac7e817fcd2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0df245dd-eca0-433c-ba34-ab6f1d36cdce"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""InGame"",
            ""id"": ""24900430-0a51-4581-9c73-a730eb9a3261"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f895876b-6779-41bb-a404-fe81c7e74807"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""5dc180b7-e384-46bb-b894-fd352e2c09b7"",
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
                    ""id"": ""ae8ec750-16f5-4c3b-b2da-79716dfa8b6e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""429012e6-8a11-4f9c-a847-f255e3786e33"",
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
                    ""id"": ""d5faee9f-450d-4e81-b7f1-c1b6a029b776"",
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
                    ""id"": ""36f79f2d-8c1f-491f-aa88-44491d930978"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""354fad75-5690-4e6a-ab2b-4a265b416357"",
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
                    ""id"": ""f9941b07-e398-4684-bf99-9e351791f8cd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4b4625d5-0f7b-4c14-8795-e905f897a682"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2ad04164-5662-41ef-a4a6-a25b469e465e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ccf4ca8b-d72e-4128-850c-b3da5523dbcb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""InSetting"",
            ""id"": ""2e579013-3cae-409b-ba2b-5d315e58e696"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""95c36da6-7edd-47ff-b273-17d7166dc22a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""868de384-8a82-425a-a55d-a7f96676045c"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InWelcome
        m_InWelcome = asset.FindActionMap("InWelcome", throwIfNotFound: true);
        m_InWelcome_Newaction = m_InWelcome.FindAction("New action", throwIfNotFound: true);
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_Move = m_InGame.FindAction("Move", throwIfNotFound: true);
        // InSetting
        m_InSetting = asset.FindActionMap("InSetting", throwIfNotFound: true);
        m_InSetting_Newaction = m_InSetting.FindAction("New action", throwIfNotFound: true);
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

    // InWelcome
    private readonly InputActionMap m_InWelcome;
    private IInWelcomeActions m_InWelcomeActionsCallbackInterface;
    private readonly InputAction m_InWelcome_Newaction;
    public struct InWelcomeActions
    {
        private @PlayerInputActions m_Wrapper;
        public InWelcomeActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_InWelcome_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_InWelcome; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InWelcomeActions set) { return set.Get(); }
        public void SetCallbacks(IInWelcomeActions instance)
        {
            if (m_Wrapper.m_InWelcomeActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_InWelcomeActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_InWelcomeActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_InWelcomeActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_InWelcomeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public InWelcomeActions @InWelcome => new InWelcomeActions(this);

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_Move;
    public struct InGameActions
    {
        private @PlayerInputActions m_Wrapper;
        public InGameActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_InGame_Move;
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
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);

    // InSetting
    private readonly InputActionMap m_InSetting;
    private IInSettingActions m_InSettingActionsCallbackInterface;
    private readonly InputAction m_InSetting_Newaction;
    public struct InSettingActions
    {
        private @PlayerInputActions m_Wrapper;
        public InSettingActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_InSetting_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_InSetting; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InSettingActions set) { return set.Get(); }
        public void SetCallbacks(IInSettingActions instance)
        {
            if (m_Wrapper.m_InSettingActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_InSettingActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_InSettingActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_InSettingActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_InSettingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public InSettingActions @InSetting => new InSettingActions(this);
    public interface IInWelcomeActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IInGameActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IInSettingActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
