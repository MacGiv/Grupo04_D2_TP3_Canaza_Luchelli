using UnityEngine;

/// <summary>
/// State behavior for when the player jumps.
/// </summary>
public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        // Apply force and consume input
        player.MovementHandler.SetVelocity(player.RB.linearVelocity.x, player.PlayerSettings.jumpForce);
        player.InputHandler.UseJumpInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Air movement
        int xInput = player.InputHandler.NormalizedInputX;
        player.MovementHandler.CheckIfShouldFlip(xInput);
        player.MovementHandler.SetVelocity(player.PlayerSettings.speed * xInput, player.RB.linearVelocity.y);

        // If vertical velocity si less or equals to 0, means that player is falling
        if (player.RB.linearVelocity.y <= 0f)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}