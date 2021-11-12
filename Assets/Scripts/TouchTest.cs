using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchTest : MonoBehaviour, PlayerInputActions.IUIActions, PlayerInputActions.IInGameActions
{
    PlayerInputActions inputActions;
    void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.InGame.SetCallbacks(this);
        inputActions.InGame.Enable();
        inputActions.UI.SetCallbacks(this);
        inputActions.UI.Enable();
        Debug.Log("Start");

        EnhancedTouchSupport.Enable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(context.control.path);
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log(context.ReadValue<Vector2>());
        }
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log(context.control.path);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(context.control.path);
        }
    }

    public void OnTimeWalkback(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(context.control.path);
        }
    }

    public void OnFetch(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log(context.control.path);
        }
    }
}
