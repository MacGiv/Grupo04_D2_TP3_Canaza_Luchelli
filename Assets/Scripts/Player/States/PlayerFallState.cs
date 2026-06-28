using UnityEngine;

/// <summary>
/// State behavior for when the player is falling or descending.
/// </summary>
public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Coyote-time jump check
        if (player.InputHandler.JumpInput && player.CanCoyoteJump())
        {
            player.ResetCoyoteTimer();
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        // Air movement
        int xInput = player.InputHandler.NormalizedInputX;
        player.MovementHandler.CheckIfShouldFlip(xInput);
        player.MovementHandler.SetVelocity(player.PlayerSettings.speed * xInput, player.RB.linearVelocity.y);

        // Check if player is moving or standing still when reaches the ground
        if (player.CheckIfGrounded())
        {
            if (xInput != 0)
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }
}