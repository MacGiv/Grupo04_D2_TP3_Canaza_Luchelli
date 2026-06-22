using UnityEngine;

/// <summary>
/// State behavior for when the player is running on the ground.
/// </summary>
public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    /// <summary>
    /// Updates the player's velocity and handles sprite flipping. Transitions to Idle/Jump/Fall.
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        int xInput = player.InputHandler.NormalizedInputX;
        player.MovementHandler.CheckIfShouldFlip(xInput);
        player.MovementHandler.SetVelocity(player.PlayerSettings.speed * xInput, player.RB.linearVelocity.y);

        if (!player.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.FallState);
            return;
        }

        if (player.InputHandler.JumpInput && player.CheckIfGrounded())
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
    }
}