public class EnemyState
{
    protected EnemyBrain enemy;
    protected EnemyStateMachine stateMachine;
    protected string animBoolName;

    public EnemyState(EnemyBrain enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        enemy.Anim.SetBool(animBoolName, true);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }
}