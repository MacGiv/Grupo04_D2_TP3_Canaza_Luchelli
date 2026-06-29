using UnityEngine;

/// <summary>
/// Core brain component for enemies. Manages references, state machine updates, 
/// reads configurations from Scriptable Objects, and calculates distances to the player.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyBrain : MonoBehaviour
{
    [Header("References")]
    public EnemySettingsSo Settings;
    public EnemyStateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform PlayerTarget { get; private set; }

    // States
    public EnemyState PatrolState { get; private set; }
    public EnemyState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyState DeathState { get; private set; }

    /// <summary> 
    /// Gets the direction the enemy is currently facing (1 = Right, -1 = Left). 
    /// </summary>
    public int FacingDirection { get; private set; } = 1;

    /// <summary> 
    /// Tracks the exact time the last attack was performed. 
    /// </summary>
    public float LastAttackTime { get; set; }

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();

        // Find the player object via Tag.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerTarget = playerObj.transform;
        }

        // Initialize States
        PatrolState = new EnemyPatrolState(this, StateMachine, "isMoving");
        ChaseState = new EnemyChaseState(this, StateMachine, "isMoving");
        AttackState = new EnemyAttackState(this, StateMachine, "isAttacking");
        DeathState = new EnemyDeathState(this, StateMachine, "isDead");

        // Start the State Machine with the Patrol State
        StateMachine.Initialize(PatrolState);
    }

    private void Update()
    {
        StateMachine.CurrentState?.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState?.PhysicsUpdate();
    }

    /// <summary>
    /// Checks if the Player is within the enemy's general vision detection range.
    /// </summary>
    public bool IsPlayerInVisionRange()
    {
        if (PlayerTarget == null) return false;
        return Vector2.Distance(transform.position, PlayerTarget.position) <= Settings.visionRange;
    }

    /// <summary>
    /// Checks if the Player is within the enemy's close combat attack range.
    /// </summary>
    public bool IsPlayerInAttackRange()
    {
        if (PlayerTarget == null) return false;
        return Vector2.Distance(transform.position, PlayerTarget.position) <= Settings.attackRange;
    }

    /// <summary>
    /// Flips the enemy's sprite and updates the horizontal orientation value.
    /// </summary>
    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    /// <summary>
    /// Bridge method triggered by an Animation Event at the end of the attack animation.
    /// </summary>
    public void OnAttackAnimationFinished()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            AttackState.FinishAttackAnimation();
        }
    }

    /// <summary>
    /// Unity editor helper that draws spheres representing vision and attack ranges.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (Settings == null) return;

        // Yellow wireframe represents detection/vision range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Settings.visionRange);

        // Red wireframe represents close melee attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Settings.attackRange);
    }
}