using UnityEngine;

/// <summary>
/// State behavior for when the player's health reaches zero.
/// Disables all input, stops movement, and triggers the death animation.
/// </summary>
public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.MovementHandler.SetVelocity(0f, 0f);
        player.IsInvulnerable = true;
    }

    public override void LogicUpdate() { } // Override Logic


    public override void PhysicsUpdate() { } // Override Physics
}