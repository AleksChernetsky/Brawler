public class BaseState
{
    protected Enemy _enemy;
    protected StateMachine _stateMachine;
    protected EnemyTracker _enemyTracker;

    public BaseState(Enemy enemy, StateMachine stateMachine, EnemyTracker enemyTracker)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;
        _enemyTracker = enemyTracker;
    }

    public virtual void EnterState()
    {
    }

    public virtual void UpdateState()
    {
    }

    public virtual void ExitState()
    {
    }
}