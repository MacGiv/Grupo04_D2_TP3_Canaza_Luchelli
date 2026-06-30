using UnityEngine;

/// <summary>
/// State behavior for when the player executes a melee combo.
/// </summary>
public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private bool comboInputBuffered;
    private bool isAnimationFinished;

    public PlayerAttackState(PlayerBrain player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.MovementHandler.SetVelocity(0, 0);
        comboCounter = 1;
        comboInputBuffered = false;
        isAnimationFinished = false;
        PlayAttackAnimation();
        player.InputHandler.UseAttackInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Input buffering
        if (player.InputHandler.AttackInput)
        {
            comboInputBuffered = true;
            player.InputHandler.UseAttackInput();
        }

        if (isAnimationFinished)
        {
            if (comboInputBuffered && comboCounter < 3)
            {
                comboCounter++;
                comboInputBuffered = false;
                isAnimationFinished = false;

                PlayAttackAnimation();
            }
            else
            {
                // If player didn't push attack or 3rd attack was already done
                if (player.CheckIfGrounded())
                    stateMachine.ChangeState(player.IdleState);
                else
                    stateMachine.ChangeState(player.FallState);
            }
        }
    }

    /// <summary>
    /// Updates the Animator with the current combo step and triggers the animation.
    /// </summary>
    private void PlayAttackAnimation()
    {
        player.Anim.SetInteger("comboCounter", comboCounter);
        player.Anim.SetTrigger("attack");

        EventBus.Publish(new SfxRequestedEvent { clip = player.PlayerSettings.attackSound });
    }

    public void FinishAttackAnimation()
    {
        isAnimationFinished = true;
    }
    
    public int GetComboCounter() => comboCounter;
}