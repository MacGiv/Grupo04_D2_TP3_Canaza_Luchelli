using UnityEngine;

/// <summary>
/// State behavior for when the player takes damage. 
/// Disables input, applies knockback, and automatically returns to idle/move after a delay.
/// </summary>
public class PlayerHitState : PlayerState
{
    private float hitTimer;
    private float stunDuration = 0.25f;
    private Vector2 knockbackForce = new Vector2(6f, 3f);

    public PlayerHitState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        hitTimer = stunDuration;
        float knockbackDirection = -player.MovementHandler.FacingDirection;
        player.MovementHandler.SetVelocity(knockbackForce.x * knockbackDirection, knockbackForce.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        hitTimer -= Time.deltaTime;
        if (hitTimer <= 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}