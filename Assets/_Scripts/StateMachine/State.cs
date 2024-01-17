public class State
{
    protected EnemyActions _enemyActions;
    protected StateMachine _stateMachine;

    public State(EnemyActions enemyActions, StateMachine stateMachine)
    {
        _enemyActions = enemyActions;
        _stateMachine = stateMachine;
    }

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}
