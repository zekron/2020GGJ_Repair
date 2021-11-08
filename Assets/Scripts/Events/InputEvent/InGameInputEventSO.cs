using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInGameInput", menuName = "Scriptable Object/Event/Input/PlayerInGameInput")]
public class InGameInputEventSO : EventChannelBaseSO, PlayerInputActions.IInGameActions
{
    public event UnityAction<Vector2> OnMoveEvent = delegate { };
    public event UnityAction OnStopMoveEvent = delegate { };

    public event UnityAction OnTimeWalkbackEvent = delegate { };
    public event UnityAction OnStopTimeWalkbackEvent = delegate { };

    public event UnityAction<bool> OnFetchEvent = delegate { };

    PlayerInputActions inputActions;

    void OnEnable()
    {
        inputActions = new PlayerInputActions();
        inputActions.InGame.SetCallbacks(this);
    }

    void OnDisable()
    {
        DisableAllInputs();
    }

    public void EnableInGameInput()
    {
        inputActions.InGame.Enable();
    }

    public void DisableAllInputs()
    {
        inputActions.InGame.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnMoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            OnStopMoveEvent.Invoke();
        }
    }

    public void OnTimeWalkback(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnTimeWalkbackEvent.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            Debug.LogError(context.phase);
            OnStopTimeWalkbackEvent.Invoke();
        }
    }

    public void OnFetch(InputAction.CallbackContext context)
    {
        Debug.LogError(context.phase);
        if (context.phase == InputActionPhase.Performed)
        {
            OnFetchEvent.Invoke(context.performed);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            OnFetchEvent.Invoke(context.performed);
        }
    }
}
