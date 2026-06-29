using UnityEngine;

/// <summary>
/// State behavior for when the enemy performs a close range attack.
/// It stops moving, triggers the attack animation, and evaluates the next state.
/// </summary>
public class EnemyAttackState : EnemyState
{
    private bool isAnimationFinished;

    public EnemyAttackState(EnemyBrain enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        enemy.RB.linearVelocity = new Vector2(0f, enemy.RB.linearVelocity.y);
        enemy.Anim.SetTrigger("attack");
        enemy.LastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Wait until the animation finishes (via Animation Event)
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }

    /// <summary>
    /// Triggered by an Animation Event on the last frame of the attack animation.
    /// </summary>
    public void FinishAttackAnimation()
    {
        isAnimationFinished = true;
    }
}