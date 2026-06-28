using UnityEngine;

/// <summary>
/// Handles all physics-based movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerBrain player;

    /// <summary>
    /// The current direction the player is facing (1 for right, -1 for left).
    /// </summary>
    public int FacingDirection { get; private set; } = 1;

    /// <summary>
    /// Workspace vector used to avoid allocating new Vector2 objects during physics updates.
    /// </summary>
    private Vector2 workspaceVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerBrain>();
    }

    /// <summary>
    /// Sets the velocity of the Rigidbody2D.
    /// </summary>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        workspaceVelocity.Set(xVelocity, yVelocity);
        rb.linearVelocity = workspaceVelocity;
    }

    /// <summary>
    /// Checks if the player needs to flip horizontally based on the input direction.
    /// </summary>
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    /// <summary>
    /// Flips the player horizontally.
    /// </summary>
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}