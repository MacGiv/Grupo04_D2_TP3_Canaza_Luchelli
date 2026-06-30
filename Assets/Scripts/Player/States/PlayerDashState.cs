using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float dashTime;
    private float dashSpeed;
    private float dashTimer;
    private float prevGravityValue;

    public PlayerDashState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        dashTime = player.PlayerSettings.dashTime;
        dashSpeed = player.PlayerSettings.dashSpeed;
        dashTimer = dashTime;
        player.InputHandler.UseDashInput();

        player.IsInvulnerable = true;
        prevGravityValue = player.RB.gravityScale;
        player.RB.gravityScale = 0f; // Avoid falling while airborne

        EventBus.Publish(new SfxRequestedEvent { clip = player.PlayerSettings.dashSound });
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.MovementHandler.SetVelocity(dashSpeed * player.MovementHandler.FacingDirection, 0f);

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            stateMachine.ChangeState(player.CheckIfGrounded() ? player.IdleState : player.FallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = prevGravityValue;
        player.IsInvulnerable = false;
    }
}