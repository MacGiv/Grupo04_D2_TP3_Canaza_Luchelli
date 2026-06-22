using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player input reading using Unity's new Input System.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    /// <summary>
    /// The raw Vector2 input from the movement keys/joystick.
    /// </summary>
    public Vector2 RawMovementInput { get; private set; }
    /// <summary>
    /// The normalized X axis input (-1 for left, 1 for right, 0 for none).
    /// </summary>
    public int NormalizedInputX { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }

    /// <summary>
    /// Callback triggered by the Player Input component when movement keys are pressed or released.
    /// </summary>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        // Normalize the input to ensure we only get -1, 0, or 1
        if (RawMovementInput.x >= 0.5f || RawMovementInput.x <= -0.5f)
        {
            NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormalizedInputX = 0;
        }
    }

    /// <summary>
    /// Callback triggered for jump actions.
    /// </summary>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }
        if (context.canceled)
        {
            JumpInput = false;
        }
    }

    /// <summary>
    /// Callback triggered for attacks.
    /// </summary>
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
        }

        if (context.canceled)
        {
            AttackInput = false;
        }
    }

    /// <summary>
    /// Consumes the jump input so it doesn't trigger multiple times.
    /// </summary>
    public void UseJumpInput() => JumpInput = false;

    /// <summary>
    /// Consumes the attack input so it doesn't trigger continuously.
    /// </summary>
    public void UseAttackInput() => AttackInput = false;
}