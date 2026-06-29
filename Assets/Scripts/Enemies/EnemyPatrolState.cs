using UnityEngine;

/// <summary>
/// State behavior for when the enemy is patrolling an area idly.
/// It moves in one direction for a set time, flips, and repeats.
/// </summary>
public class EnemyPatrolState : EnemyState
{
    private float patrolTimer;
    private float timeBeforeFlip = 3f;

    public EnemyPatrolState(EnemyBrain enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        patrolTimer = timeBeforeFlip;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check for player detection
        if (enemy.IsPlayerInVisionRange())
        {
            stateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        // Handle patrol timer
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0)
        {
            enemy.Flip(); 
            patrolTimer = timeBeforeFlip;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Move the enemy
        enemy.RB.linearVelocity = new Vector2(enemy.Settings.patrolSpeed * enemy.FacingDirection, enemy.RB.linearVelocity.y);
    }
}