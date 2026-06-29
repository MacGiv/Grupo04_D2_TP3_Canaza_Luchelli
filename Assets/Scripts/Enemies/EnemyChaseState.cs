using UnityEngine;

/// <summary>
/// State behavior for when the enemy detects the player and actively pursues them.
/// The enemy moves at a higher speed and dynamically flips to face the player.
/// </summary>
public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyBrain enemy, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 1. Check if the player escaped our vision range
        if (!enemy.IsPlayerInVisionRange())
        {
            stateMachine.ChangeState(enemy.PatrolState);
            return;
        }

        // 2. Check if we are close enough to attack AND cooldown is over
        if (enemy.IsPlayerInAttackRange())
        {
            if (Time.time >= enemy.LastAttackTime + enemy.Settings.attackCooldown)
            {
                stateMachine.ChangeState(enemy.AttackState);
                return;
            }
            else
            {
                // Stop enemy from pushing the player
                enemy.RB.linearVelocity = new Vector2(0f, enemy.RB.linearVelocity.y);
                return;
            }
        }

        // 3. Keep facing the player
        FacePlayer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Move towards the player using chaseSpeed
        enemy.RB.linearVelocity = new Vector2(enemy.Settings.chaseSpeed * enemy.FacingDirection, enemy.RB.linearVelocity.y);
    }

    /// <summary>
    /// Compares the X positions of the enemy and the player to determine if a flip is needed.
    /// </summary>
    private void FacePlayer()
    {
        float playerX = enemy.PlayerTarget.position.x;
        float enemyX = enemy.transform.position.x;

        // Si el jugador está a la derecha y miramos a la izquierda, o viceversa, giramos.
        if (playerX > enemyX && enemy.FacingDirection == -1)
        {
            enemy.Flip();
        }
        else if (playerX < enemyX && enemy.FacingDirection == 1)
        {
            enemy.Flip();
        }
    }
}