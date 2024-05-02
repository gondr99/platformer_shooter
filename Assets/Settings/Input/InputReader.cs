using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private Controls _controls;

    public Action JumpKeyEvent;
    public Action<int> OnCharacterChangeEvent;

    public Action<bool> OnFireKeyEvent;
    public Action OnReloadKeyEvent;

    public Vector2 Movement { get; private set; }
    public Vector2 MousePos { get; private set; }


    //method call, when SO is initialized, Game Start
    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
        }

        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpKeyEvent?.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();
        MousePos = Camera.main.ScreenToWorldPoint(screenPos);
    }

    public void OnChangeOne(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnCharacterChangeEvent?.Invoke(0);
    }

    public void OnChangeTwo(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnCharacterChangeEvent?.Invoke(1);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnFireKeyEvent?.Invoke(true);
        if (context.canceled)
            OnFireKeyEvent?.Invoke(false);
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnReloadKeyEvent?.Invoke();
    }
}
