using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { private set; get; }
    
    public event EventHandler OnInteraction;
    public event EventHandler OnAlternativeInteraction;
    public event EventHandler OnPauseInteraction;

    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        Instance = this;
        
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Interact.performed += InteractPerformed;
        _playerInputActions.Player.InteractAlternative.performed += InteractAlternativePerformed;

        _playerInputActions.Player.Pause.performed += PausePerformed;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= InteractPerformed;
        _playerInputActions.Player.InteractAlternative.performed -= InteractAlternativePerformed;

        _playerInputActions.Player.Pause.performed -= PausePerformed;
        
        _playerInputActions.Dispose();
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternativePerformed(InputAction.CallbackContext obj)
    {
        OnAlternativeInteraction?.Invoke(this, EventArgs.Empty);
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseInteraction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
        
        return inputVector.normalized;
    }
}
