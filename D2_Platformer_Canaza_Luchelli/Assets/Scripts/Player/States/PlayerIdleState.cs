using UnityEngine;

/// <summary>
/// State behavior for when the player is standing still on the ground.
/// </summary>
public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    /// <summary>
    /// Stops the player's horizontal movement upon entering the state.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        // Set X velocity to 0, maintain current Y velocity (falling/gravity)
        player.MovementHandler.SetVelocity(0f, player.RB.linearVelocity.y);
    }

    /// <summary>
    /// Checks for movement input to transition to the Move/Fall/Jump state.
    /// </summary>
    public override void LogicUpdate()
    {
        base.LogicUpdate();

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

        if (player.InputHandler.NormalizedInputX != 0)
        {
            stateMachine.ChangeState(player.MoveState);
            return;
        }
    }
}