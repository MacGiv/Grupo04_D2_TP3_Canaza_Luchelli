using UnityEngine;

/// <summary>
/// Core controller for the player. Manages the state machine, physics, and component references.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovementHandler), typeof(PlayerCombatHandler))]
public class PlayerBrain : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    // States
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDashState DashState { get; private set; }

    // Cached Components
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public PlayerMovementHandler MovementHandler { get; private set; }
    public PlayerCombatHandler CombatHandler { get; private set; }

    [Header("Player Data")]
    [SerializeField] private PlayerSettingsSo playerSettings;
    
    [Header("Collision Checks")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask whatIsGround;
    public PlayerSettingsSo PlayerSettings => playerSettings;
    public int FacingDirection { get; private set; } = 1;
    public bool IsInvulnerable { get; set; }
    private float coyoteTimer;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        MovementHandler = GetComponent<PlayerMovementHandler>();
        CombatHandler = GetComponent<PlayerCombatHandler>();

        // Initialize states and pass the corresponding animation parameter names
        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "move");
        JumpState = new PlayerJumpState(this, StateMachine, "jump");
        FallState = new PlayerFallState(this, StateMachine, "fall");
        AttackState = new PlayerAttackState(this, StateMachine, "attack");
        DashState = new PlayerDashState(this, StateMachine, "dash");
    }

    private void Start()
    {
        // Start the state machine in the Idle state
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState?.LogicUpdate();
        
        // Coyote time control
        if (CheckIfGrounded())
        {
            coyoteTimer = PlayerSettings.coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState?.PhysicsUpdate();
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    /// <summary>
    /// Checks if the player is touching the ground layer.
    /// </summary>
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// Bridge method triggered by an Animation Event at the last frame of the attack animation.
    /// </summary>
    public void OnAttackAnimationFinished()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            AttackState.FinishAttackAnimation();
        }
    }

    /// <summary>
    /// Bridge method triggered by an Animation Event at the exact frame the sword swings.
    /// </summary>
    public void TriggerMeleeHitbox()
    {
        if (StateMachine.CurrentState == AttackState)
        {
            int currentCombo = AttackState.GetComboCounter();
            CombatHandler.CheckMeleeHit(currentCombo);
        }
    }

    /// <summary>
    /// Evaluates if the player is within the grace period to execute a jump after leaving a ledge.
    /// </summary>
    public bool CanCoyoteJump() => coyoteTimer > 0f;

    /// <summary>
    /// Consumes the coyote time so it cannot be reused in the same airborne session.
    /// </summary>
    public void ResetCoyoteTimer() => coyoteTimer = 0f;
}