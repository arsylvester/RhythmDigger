// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Objects/PlayerInputActions.inputactions'

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
            ""name"": ""Player Controls"",
            ""id"": ""c4c6e3b3-1e6c-4b42-aa38-0a139e2d0504"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8bed9e7a-6701-44ae-baeb-bdc9b5bc5af4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button1"",
                    ""type"": ""Button"",
                    ""id"": ""e38e2dc4-851f-4b25-9daf-efe55bde1157"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button2"",
                    ""type"": ""Button"",
                    ""id"": ""e7b9ed61-e2db-46e5-a0b6-b6ee291090d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button3"",
                    ""type"": ""Button"",
                    ""id"": ""03e2d9d1-8cdf-4b3c-a34b-309d3dc67914"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button1Release"",
                    ""type"": ""Button"",
                    ""id"": ""85d53136-2312-40c7-af09-696f8bc1bd7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Button2Release"",
                    ""type"": ""Button"",
                    ""id"": ""c6c02656-c43c-4eb9-a6d4-d941dd2194e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""23cd072e-66a7-4be1-a899-eb9aae34c68c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MovePressUp"",
                    ""type"": ""Button"",
                    ""id"": ""8ebcfaa4-f3b9-4b00-a7c0-2b08e0c4bac1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovePressDown"",
                    ""type"": ""Button"",
                    ""id"": ""fdfbec32-6c80-4fa9-be53-650d4bfaffe7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovePressLeft"",
                    ""type"": ""Button"",
                    ""id"": ""38f613ac-6416-43b6-be86-dc6a106b7234"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovePressRight"",
                    ""type"": ""Button"",
                    ""id"": ""edba31fd-cac3-46cf-933a-b57cd66779c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""64f079c3-25c0-4ae0-bcf4-14cebead6ae8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Any"",
                    ""type"": ""Button"",
                    ""id"": ""bdf46380-6a22-4a03-b6c2-bdb355a5b108"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""1f7fdd08-4063-41ff-8e4a-a309eb6174c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5c78c9f1-d3dc-4ed1-b761-193d2bfc99a5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""86877ddc-d488-47f2-8024-2e15bd19fa8e"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9d35265a-6a43-4bb9-8135-9d23db372b1d"",
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
                    ""id"": ""ef60ccc2-5b6f-465b-9da0-9434a0152d2d"",
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
                    ""id"": ""59f0a39d-fe5b-4d87-8752-233a15833fa8"",
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
                    ""id"": ""05ce7af6-4f83-4b7f-bdd8-b0da43382f95"",
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
                    ""id"": ""e0bb629b-4329-4f97-9db2-5ea79c1916c4"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d8b57df7-7c5c-4b9a-8968-bd7ae17ba322"",
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
                    ""id"": ""515d3b74-f976-4359-a887-3c293abd622b"",
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
                    ""id"": ""6547da59-6d6b-447e-b153-050f0ecbdea7"",
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
                    ""id"": ""135735b8-a1bb-4cce-b017-362b424a03ee"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""011d52a6-e5ea-422e-bccc-0abaae415a7c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""908fe55a-289d-48c6-aae3-9016a7f1e925"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7557811-6399-43d7-bc33-b16262667601"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cb6f233-be24-4d6c-9a38-63bfcb6e35aa"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c17a1ed2-adc6-4daf-9ad1-b06f7dae0b0a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdf07d95-e186-496c-a53e-23a171fda7a0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b38b2332-bede-4d60-ab1b-5c4b6355b4ff"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d574f8cc-8abb-418b-a5e5-18e546188929"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePressDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07f1fd2d-36c9-4493-9d74-4eaa6867938b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePressLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12b8eae1-4a87-4702-8668-8cbb8ff55b69"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePressRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ea0a7d4-2f2d-402b-91ee-2a0441a92ca2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePressUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cf53f7b-b929-4403-a803-6030d08264be"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00ee3887-12e5-45cf-b6d6-00ba1f42711f"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05ac96e1-33b4-4722-8931-1701c6f6309b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0eaa9ea4-ed35-4545-b6b3-6c83c1566578"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9749ad7e-4e14-4f07-88d9-4bba6c687347"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_Button1 = m_PlayerControls.FindAction("Button1", throwIfNotFound: true);
        m_PlayerControls_Button2 = m_PlayerControls.FindAction("Button2", throwIfNotFound: true);
        m_PlayerControls_Button3 = m_PlayerControls.FindAction("Button3", throwIfNotFound: true);
        m_PlayerControls_Button1Release = m_PlayerControls.FindAction("Button1Release", throwIfNotFound: true);
        m_PlayerControls_Button2Release = m_PlayerControls.FindAction("Button2Release", throwIfNotFound: true);
        m_PlayerControls_Reset = m_PlayerControls.FindAction("Reset", throwIfNotFound: true);
        m_PlayerControls_MovePressUp = m_PlayerControls.FindAction("MovePressUp", throwIfNotFound: true);
        m_PlayerControls_MovePressDown = m_PlayerControls.FindAction("MovePressDown", throwIfNotFound: true);
        m_PlayerControls_MovePressLeft = m_PlayerControls.FindAction("MovePressLeft", throwIfNotFound: true);
        m_PlayerControls_MovePressRight = m_PlayerControls.FindAction("MovePressRight", throwIfNotFound: true);
        m_PlayerControls_Test = m_PlayerControls.FindAction("Test", throwIfNotFound: true);
        m_PlayerControls_Any = m_PlayerControls.FindAction("Any", throwIfNotFound: true);
        m_PlayerControls_Quit = m_PlayerControls.FindAction("Quit", throwIfNotFound: true);
        m_PlayerControls_Scroll = m_PlayerControls.FindAction("Scroll", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_Button1;
    private readonly InputAction m_PlayerControls_Button2;
    private readonly InputAction m_PlayerControls_Button3;
    private readonly InputAction m_PlayerControls_Button1Release;
    private readonly InputAction m_PlayerControls_Button2Release;
    private readonly InputAction m_PlayerControls_Reset;
    private readonly InputAction m_PlayerControls_MovePressUp;
    private readonly InputAction m_PlayerControls_MovePressDown;
    private readonly InputAction m_PlayerControls_MovePressLeft;
    private readonly InputAction m_PlayerControls_MovePressRight;
    private readonly InputAction m_PlayerControls_Test;
    private readonly InputAction m_PlayerControls_Any;
    private readonly InputAction m_PlayerControls_Quit;
    private readonly InputAction m_PlayerControls_Scroll;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @Button1 => m_Wrapper.m_PlayerControls_Button1;
        public InputAction @Button2 => m_Wrapper.m_PlayerControls_Button2;
        public InputAction @Button3 => m_Wrapper.m_PlayerControls_Button3;
        public InputAction @Button1Release => m_Wrapper.m_PlayerControls_Button1Release;
        public InputAction @Button2Release => m_Wrapper.m_PlayerControls_Button2Release;
        public InputAction @Reset => m_Wrapper.m_PlayerControls_Reset;
        public InputAction @MovePressUp => m_Wrapper.m_PlayerControls_MovePressUp;
        public InputAction @MovePressDown => m_Wrapper.m_PlayerControls_MovePressDown;
        public InputAction @MovePressLeft => m_Wrapper.m_PlayerControls_MovePressLeft;
        public InputAction @MovePressRight => m_Wrapper.m_PlayerControls_MovePressRight;
        public InputAction @Test => m_Wrapper.m_PlayerControls_Test;
        public InputAction @Any => m_Wrapper.m_PlayerControls_Any;
        public InputAction @Quit => m_Wrapper.m_PlayerControls_Quit;
        public InputAction @Scroll => m_Wrapper.m_PlayerControls_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Button1.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton1;
                @Button1.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton1;
                @Button1.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton1;
                @Button2.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton2;
                @Button2.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton2;
                @Button2.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton2;
                @Button3.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton3;
                @Button3.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton3;
                @Button3.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton3;
                @Button1Release.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton1Release;
                @Button1Release.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton1Release;
                @Button1Release.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton1Release;
                @Button2Release.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton2Release;
                @Button2Release.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton2Release;
                @Button2Release.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnButton2Release;
                @Reset.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReset;
                @MovePressUp.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressUp;
                @MovePressUp.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressUp;
                @MovePressUp.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressUp;
                @MovePressDown.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressDown;
                @MovePressDown.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressDown;
                @MovePressDown.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressDown;
                @MovePressLeft.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressLeft;
                @MovePressLeft.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressLeft;
                @MovePressLeft.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressLeft;
                @MovePressRight.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressRight;
                @MovePressRight.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressRight;
                @MovePressRight.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovePressRight;
                @Test.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnTest;
                @Any.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAny;
                @Any.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAny;
                @Any.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAny;
                @Quit.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnQuit;
                @Scroll.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnScroll;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Button1.started += instance.OnButton1;
                @Button1.performed += instance.OnButton1;
                @Button1.canceled += instance.OnButton1;
                @Button2.started += instance.OnButton2;
                @Button2.performed += instance.OnButton2;
                @Button2.canceled += instance.OnButton2;
                @Button3.started += instance.OnButton3;
                @Button3.performed += instance.OnButton3;
                @Button3.canceled += instance.OnButton3;
                @Button1Release.started += instance.OnButton1Release;
                @Button1Release.performed += instance.OnButton1Release;
                @Button1Release.canceled += instance.OnButton1Release;
                @Button2Release.started += instance.OnButton2Release;
                @Button2Release.performed += instance.OnButton2Release;
                @Button2Release.canceled += instance.OnButton2Release;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
                @MovePressUp.started += instance.OnMovePressUp;
                @MovePressUp.performed += instance.OnMovePressUp;
                @MovePressUp.canceled += instance.OnMovePressUp;
                @MovePressDown.started += instance.OnMovePressDown;
                @MovePressDown.performed += instance.OnMovePressDown;
                @MovePressDown.canceled += instance.OnMovePressDown;
                @MovePressLeft.started += instance.OnMovePressLeft;
                @MovePressLeft.performed += instance.OnMovePressLeft;
                @MovePressLeft.canceled += instance.OnMovePressLeft;
                @MovePressRight.started += instance.OnMovePressRight;
                @MovePressRight.performed += instance.OnMovePressRight;
                @MovePressRight.canceled += instance.OnMovePressRight;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
                @Any.started += instance.OnAny;
                @Any.performed += instance.OnAny;
                @Any.canceled += instance.OnAny;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnButton1(InputAction.CallbackContext context);
        void OnButton2(InputAction.CallbackContext context);
        void OnButton3(InputAction.CallbackContext context);
        void OnButton1Release(InputAction.CallbackContext context);
        void OnButton2Release(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
        void OnMovePressUp(InputAction.CallbackContext context);
        void OnMovePressDown(InputAction.CallbackContext context);
        void OnMovePressLeft(InputAction.CallbackContext context);
        void OnMovePressRight(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
        void OnAny(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
}
