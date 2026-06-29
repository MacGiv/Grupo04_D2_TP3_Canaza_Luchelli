using UnityEngine;

/// <summary>
/// State behavior for when the enemy dies. 
/// Stops all movement, plays the death animation, and disables colliders.
/// </summary>
public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyBrain enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.RB.linearVelocity = Vector2.zero;
        enemy.RB.gravityScale = 0f;
        Collider2D[] colliders = enemy.GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }
}